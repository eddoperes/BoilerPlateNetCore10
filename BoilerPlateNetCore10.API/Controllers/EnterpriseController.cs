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
            try
            {
                var enterpriseDTOs = await _enterpriseService.GetAllAsync();
                return Ok(enterpriseDTOs);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
                //return ReportUnexpectedException(ex);
            }
        }
        

        [HttpGet("{id}", Name = "GetEnterprise")]
        //[AllowAnonymous]
        public async Task<ActionResult<EnterpriseDTO>> Get(int id)
        {
            try
            {
                var collectionItemDTO = await _enterpriseService.GetByIdAsync(id);
                if (collectionItemDTO == null)
                    return NotFound();
                return Ok(collectionItemDTO);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
                //return ReportUnexpectedException(ex);
            }
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult<EnterpriseDTO>> Post([FromBody] EnterpriseDTO enterpriseDTO)
        {
            try
            {
                if (enterpriseDTO == null)
                    return BadRequest();
                var addedEnterpriseDTO = await _enterpriseService.AddAsync(enterpriseDTO);
                return new CreatedAtRouteResult("GetEnterprise", new { id = addedEnterpriseDTO.Id }, addedEnterpriseDTO);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
                //return ReportUnexpectedException(ex);
            }
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult<EnterpriseDTO>> Put(int id, [FromBody] EnterpriseDTO enterpriseDTO)
        {
            try
            {
                if (enterpriseDTO == null)
                    return BadRequest();
                if (enterpriseDTO.Id != id)
                    return BadRequest();
                await _enterpriseService.UpdateAsync(enterpriseDTO);
                return Ok(enterpriseDTO);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
                //return ReportUnexpectedException(ex);
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator,Strong,Player")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _enterpriseService.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
                //return ReportUnexpectedException(ex);
            }
        }

        /*
        private ActionResult ReportUnexpectedException(Exception ex)
        {
            Log.Logger = _logger;
            Log.Error(ex.Message);
            var innerException = ex.InnerException;
            while (innerException != null)
            {
                Log.Error(innerException.Message);
                innerException = innerException.InnerException;
            }
            return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
        }
        */


    }
}
