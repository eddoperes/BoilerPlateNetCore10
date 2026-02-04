using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces.Super;
using System.Threading.Tasks;

namespace BoilerPlateNetCore10.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {

        Task<User?> ValidateCredentialsByLoginAsync(string login, string password);

        Task<bool> RevokeTokenAsync(long userId);
        Task<User?> GetByLoginAsync(string login);

        Task<bool> ExistsLoginAsync(string login);
        Task<bool> ExistsLoginWithOtherIdAsync(long id, string login);

    }
}
