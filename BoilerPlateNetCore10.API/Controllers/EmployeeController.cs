using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoilerPlateNetCore10.API.Controllers
{

    //[ApiVersion("1")]
    [ApiController]
    //[Authorize]
    //[Route("api/[controller]/v{version:apiVersion}")]
    [Route("api/[controller]")]

    public class EmployeeController : Controller
    {

        private readonly IEmployeeService _employeeService;
        //private readonly Serilog.ILogger _logger;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            //_logger = logger;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> Get()
        {
            var employeeDTOs = await _employeeService.GetAllAsync();
            return Ok(employeeDTOs);
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        //[AllowAnonymous]
        public async Task<ActionResult<EmployeeDTO>> Get(int id)
        {
            var employeeDTO = await _employeeService.GetByIdAsync(id);
            if (employeeDTO == null)
                return NotFound();
            return Ok(employeeDTO);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult<EmployeeDTO>> Post([FromBody] EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
                return BadRequest();
            var addedEmployeeDTO = await _employeeService.AddAsync(employeeDTO);
            return new CreatedAtRouteResult("GetEmployee", new { id = addedEmployeeDTO.Id }, addedEmployeeDTO);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult<EmployeeDTO>> Put(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
                return BadRequest();
            if (employeeDTO.Id != id)
                return BadRequest();
            await _employeeService.UpdateAsync(employeeDTO);
            return Ok(employeeDTO);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult> Delete(int id)
        {
            await _employeeService.RemoveAsync(id);
            return NoContent();
        }

    }
}
