using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetResponse<T>(IDataResult<T> result)
        {
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetResponse(IResult result)
        {
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        // [NonAction]
        public IActionResult GetResponse<T>(IDataResult<T> result, int total)
        {
            if (result.Success)
            {
                return Ok(new { data = new { data = result.Data, count = total }, result.Message, result.Success });
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}