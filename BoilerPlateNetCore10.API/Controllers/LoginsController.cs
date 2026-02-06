using BoilerPlateNetCore10.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BoilerPlateNetCore10.API.Models;
using BoilerPlateNetCore10.Application.DTOs;

namespace BoilerPlateNetCore10.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : Controller
    {

        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginsController(ILoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<TokenDTO>> Login([FromBody] LoginModel userInfo)
        {            
            var result = await _loginService.ValidateCredentialsWithLogin(userInfo.Login, userInfo.Password,
                                                                          _configuration["Jwt:SecretKey"]!,
                                                                          int.Parse(_configuration["Jwt:Minutes"]!),
                                                                          _configuration["Jwt:Issuer"]!,
                                                                          _configuration["Jwt:Audience"]!,
                                                                          int.Parse(_configuration["Jwt:DaysToExpire"]!));
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid login attempt");
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginWithRefreshToken")]
        public async Task<ActionResult<TokenDTO>> LoginWithRefreshToken([FromBody] LoginTokenModel tokenInfo)
        {
            var result = await _loginService.ValidateCredentialsWithToken(tokenInfo.Token, tokenInfo.RefreshToken,
                                                                          _configuration["Jwt:SecretKey"]!,
                                                                          int.Parse(_configuration["Jwt:Minutes"]!),
                                                                          _configuration["Jwt:Issuer"]!,
                                                                          _configuration["Jwt:Audience"]!);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid login attempt");
            }           
        }

        [AllowAnonymous]
        [HttpPost("RevokeToken")]
        public async Task<ActionResult<RevokeTokenModel>> RevokeToken([FromBody] RevokeTokenModel userInfo)
        {           
            var result = await _loginService.RevokeToken(userInfo.UserId);
            if (result)
            {
                return Ok(userInfo);
            }
            else
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid revoke attempt");
            }            
        }       

    }
}
