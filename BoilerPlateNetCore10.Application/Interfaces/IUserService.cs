using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.Interfaces.Super;
using System.Threading.Tasks;

namespace BoilerPlateNetCore10.Application.Interfaces
{
    public interface IUserService : ICrudService<UserDTO>
    {

        Task<bool> ExistsLoginAsync(string login);
        Task<bool> ExistsLoginWithOtherIdAsync(long id, string login);

    }
}
