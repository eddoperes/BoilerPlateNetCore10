using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.Interfaces;
using BoilerPlateNetCore10.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Serilog;

namespace BoilerPlateNetCore10.API.Controllers
{

    //[ApiVersion("1")]
    [ApiController]
    //[Authorize]
    //[Route("api/[controller]/v{version:apiVersion}")]
    [Route("api/[controller]")]
    public class EnterpriseController : Controller
    {

        private readonly IEnterpriseService _enterpriseService;
        //private readonly Serilog.ILogger _logger;

        public EnterpriseController(IEnterpriseService enterpriseService)
        {
            _enterpriseService = enterpriseService;
            //_logger = logger;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EnterpriseDTO>>> Get()
        {
            var enterpriseDTOs = await _enterpriseService.GetAllAsync();
            return Ok(enterpriseDTOs);
        }        

        [HttpGet("{id}", Name = "GetEnterprise")]
        //[AllowAnonymous]
        public async Task<ActionResult<EnterpriseDTO>> Get(int id)
        {
            var enterpriseDTO = await _enterpriseService.GetByIdAsync(id);
            if (enterpriseDTO == null)
                return NotFound();
            return Ok(enterpriseDTO);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult<EnterpriseDTO>> Post([FromBody] EnterpriseDTO enterpriseDTO)
        {
            if (enterpriseDTO == null)
                return BadRequest();
            var addedEnterpriseDTO = await _enterpriseService.AddAsync(enterpriseDTO);
            return new CreatedAtRouteResult("GetEnterprise", new { id = addedEnterpriseDTO.Id }, addedEnterpriseDTO);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult<EnterpriseDTO>> Put(int id, [FromBody] EnterpriseDTO enterpriseDTO)
        {
            if (enterpriseDTO == null)
                return BadRequest();
            if (enterpriseDTO.Id != id)
                return BadRequest();
            await _enterpriseService.UpdateAsync(enterpriseDTO);
            return Ok(enterpriseDTO);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult> Delete(int id)
        {
            await _enterpriseService.RemoveAsync(id);
            return NoContent();
        }

    }
}
