using Microsoft.EntityFrameworkCore;
using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces;
using BoilerPlateNetCore10.Infra.Data.Context;
using BoilerPlateNetCore10.Infra.Data.Repository.Super;
using System.Security.Cryptography;
using System.Text;

namespace BoilerPlateNetCore10.Infra.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        #region "Crud"

        public new async Task<User> CreateAsync(User user)
        {
            if (user.SensitiveData != null)
            {
                var pass = ComputHash(user.SensitiveData.Password, SHA256.Create());
                user.UpdatePasswordWithEncryptedVersion(pass);
            }
            return await base.CreateAsync(user);
        }

        public new async Task<User> UpdateAsync(User user)
        {
            if (user.SensitiveData != null) //Sensitive data is loaded
            {
                if (user.SensitiveData.Password.Length < 50) //But not encrypted
                {
                    var pass = ComputHash(user.SensitiveData.Password, SHA256.Create());
                    user.UpdatePasswordWithEncryptedVersion(pass); //So encrypt
                }
            }           
            return await base.UpdateAsync(user);
        }

        #endregion

        #region "Login"

        public async Task<bool> ExistsLoginAsync(string login)
        {
            return await _dataSet.AnyAsync(i => i.Login.Equals(login));
        }

        public async Task<bool> ExistsLoginWithOtherIdAsync(long id, string login)
        {
            return await _dataSet.AnyAsync(i => i.Login.Equals(login) && i.Id != id);
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _dataSet.Where(m => m.Login == login)
                                 .FirstOrDefaultAsync();
        }

        public async Task<bool> RevokeTokenAsync(long userId)
        {
            var user = _dataSet.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return false;
            user.UpdateRefreshToken("");
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User?> ValidateCredentialsByLoginAsync(string login, string password)
        {
            var pass = ComputHash(password, SHA256.Create());
            return await _dataSet.Include(u => u.SensitiveData).FirstOrDefaultAsync(u => u.Login == login && u.SensitiveData!.Password == pass);
        }

        # endregion

        #region "Auxiliary"

        private static string ComputHash(string input, SHA256 algorithm)
        {
            byte[] inpuBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(inpuBytes);
            return BitConverter.ToString(hashedBytes);
        }

        #endregion

    }
}
