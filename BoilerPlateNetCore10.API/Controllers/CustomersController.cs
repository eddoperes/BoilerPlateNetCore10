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
    public class CustomersController : Controller
    {


        private readonly ICustomerService _customerService;
        //private readonly Serilog.ILogger _logger;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
            //_logger = logger;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> Get()
        {
            var customerDTOs = await _customerService.GetAllAsync();
            return Ok(customerDTOs);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        //[AllowAnonymous]
        public async Task<ActionResult<CustomerDTO>> Get(int id)
        {
            var customerDTO = await _customerService.GetByIdAsync(id);
            if (customerDTO == null)
                return NotFound();
            return Ok(customerDTO);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult<CustomerDTO>> Post([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
                return BadRequest();
            var addedCustomerDTO = await _customerService.AddAsync(customerDTO);
            return new CreatedAtRouteResult("GetCustomer", new { id = addedCustomerDTO.Id }, addedCustomerDTO);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult<CustomerDTO>> Put(int id, [FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
                return BadRequest();
            if (customerDTO.Id != id)
                return BadRequest();
            await _customerService.UpdateAsync(customerDTO);
            return Ok(customerDTO);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult> Delete(int id)
        {
            await _customerService.RemoveAsync(id);
            return NoContent();
        }


    }
}
