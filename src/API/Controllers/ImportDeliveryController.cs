using Application.Features.Deliveries.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// ImportDelivery Controller Class
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ImportDeliveryController : ControllerBase
    {
        /// <summary>
        /// Insert an import file
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="file"></param>
        /// <returns>Status 200 OK</returns>
        /// <returns>Status 400 BadRequest</returns>
        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(
            [FromServices] IMediator mediator, 
            IFormFile file)
        {
            var result = await mediator.Send(new InsertImportDeliveryCommand { File = file });
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }


        /// <summary>
        /// Get All Import
        /// </summary>
        /// <returns>Status 200 Ok</returns>
        [HttpGet]
        [Route("GetAllImports")]
        public async Task<IActionResult> GetAllImports([FromServices] IMediator mediator)
        {
            var imports = "";
            //var imports = await mediator.Send(new GetAllDeliveriesImport());
            return Ok(imports);
        }

        /// <summary>
        /// Get a Import By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById([FromServices] IMediator mediator, Guid id)
        {
            var import = "";
            //var import = await mediator.Send(new GetImportByIdQuery() { Id = id });
            return Ok(import);
        }
    }
}
