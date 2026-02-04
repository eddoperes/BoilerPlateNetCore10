using BoilerPlateNetCore10.Application.DTOs;
using System.Threading.Tasks;

namespace BoilerPlateNetCore10.Application.Interfaces
{
    public interface ILoginService
    {

        Task<TokenDTO?> ValidateCredentialsWithLogin(string nickName, string password,
                                                     string secretKey, double minutes,
                                                     string issuer, string audience,
                                                     int daysToExpire);

        Task<TokenDTO?> ValidateCredentialsWithToken(string token, string refreshToken,
                                                     string secretKey, double minutes,
                                                     string issuer, string audience);

        Task<bool> RevokeToken(long userId);

    }
}
