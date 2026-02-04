using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.Interfaces;
using BoilerPlateNetCore10.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BoilerPlateNetCore10.Domain.Entities;

namespace BoilerPlateNetCore10.Application.Services
{
    public class LoginService : ILoginService
    {

        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<TokenDTO?> ValidateCredentialsWithLogin(string nickName, string password, 
                                                                  string secretKey, double minutes, 
                                                                  string issuer, string audience, 
                                                                  int daysToExpire)
        {

            var user = await _userRepository.ValidateCredentialsByLoginAsync(nickName, password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, nickName),
                    new Claim(ClaimTypes.Role, user.PermissionLevel)
                };
                var accessToken = GenerateAccessToken(claims, secretKey, minutes, issuer, audience);
                var refreshToken = GenerateRefreshToken();
                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate.AddMinutes(minutes);
                   
                user.UpdateLoginInfo(refreshToken, DateTime.Now.AddDays(daysToExpire));                
                await _userRepository.UpdateAsync(user);
                return new TokenDTO(user.Id,
                                    user.Name,
                                    true,
                                    createDate.ToString(DATE_FORMAT),
                                    expirationDate.ToString(DATE_FORMAT),
                                    accessToken,
                                    refreshToken,
                                    user.PermissionLevel);
            }            
            else
            {
                return null;
            }
        }

      
        public async Task<TokenDTO?> ValidateCredentialsWithToken(string accessToken, string refreshToken,  
                                                                  string secretKey, double minutes, 
                                                                  string issuer, string audience)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken, secretKey);
            var identityName = principal.Identity!.Name;
            var newAccessToken = GenerateAccessToken(principal.Claims, secretKey, minutes, issuer, audience);
            var newRefreshToken = GenerateRefreshToken();
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(minutes);

            User? user= await _userRepository.GetByLoginAsync(identityName!);
              
            if (user != null)
            {
                if (user.RefreshToken != refreshToken || user.RefreshTokenExpire <= DateTime.Now)
                    return null;
                user.UpdateRefreshToken(newRefreshToken);
                await _userRepository.UpdateAsync(user);
                return new TokenDTO(user.Id,
                                    user.Name,
                                    true,
                                    createDate.ToString(DATE_FORMAT),
                                    expirationDate.ToString(DATE_FORMAT),
                                    newAccessToken,
                                    newRefreshToken,
                                    user.PermissionLevel);
            }           
            else
            {
                return null;
            }
        }

        public async Task<bool> RevokeToken(long userId)
        {   
            return await _userRepository.RevokeTokenAsync(userId);                               
        }

        private string GenerateAccessToken(IEnumerable<Claim> claims, string secretKey, 
                                           double minutes, string issuer, string audience)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(minutes);
            var options = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(options);
            return tokenString;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string secretKey)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (securityToken == null ||
                !jwtSecurityToken!.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

    }
}
