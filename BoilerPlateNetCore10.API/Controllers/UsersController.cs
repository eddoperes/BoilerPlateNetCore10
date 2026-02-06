using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoilerPlateNetCore10.API.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private readonly IUserService _userService;
        private readonly Serilog.ILogger _logger;

        public UsersController(IUserService userService, Serilog.ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var userDTOs = await _userService.GetAllAsync();
            return Ok(userDTOs);
        }

        [HttpGet("{id}", Name = "GetUser")]
        //[Authorize(Roles = "Administrator,Player")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var userDTO = await _userService.GetByIdAsync(id);
            if (userDTO == null)
                return NotFound();
            return Ok(userDTO);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> Post([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Request body can't be null");
            if (userDTO.SensitiveData == null)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Sensitive data can't be null");
            if (await _userService.ExistsLoginAsync(userDTO.Login))
                return Problem(statusCode: StatusCodes.Status409Conflict, detail: "Login already exists");
            var newUserDTO = await _userService.AddAsync(userDTO);
            return new CreatedAtRouteResult("GetUser", new { id = newUserDTO.Id }, newUserDTO);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator,Player")]
        public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] UserDTO userDTO)
        {                   
            if (userDTO == null)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Request body can't be null");
            if (userDTO.Id != id)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Id must be the same in request body");
            if (await _userService.ExistsLoginWithOtherIdAsync(userDTO.Id, userDTO.Login))
                return Problem(statusCode: StatusCodes.Status409Conflict, detail: "Login already exists in other id");
            var editedUserDTO = await _userService.UpdateAsync(userDTO);
            if (editedUserDTO == null)
                return NotFound();
            return Ok(editedUserDTO);            
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.RemoveAsync(id);
            return NoContent();
        }

    }
}
