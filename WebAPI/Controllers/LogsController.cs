using Business.Handlers.Logs.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : BaseApiController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await Mediator.Send(new GetLogDtoQuery()));
        }
    }
}