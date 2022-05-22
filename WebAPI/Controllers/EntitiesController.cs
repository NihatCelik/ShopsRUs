using Business.Handlers.Entities.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController : BaseApiController
    {
        [HttpGet("getlookupbyentitygroupid")]
        public async Task<IActionResult> GetLookupByEntityGroupId(int entityGroupId)
        {
            return GetResponse(await Mediator.Send(new GetEntitesLookUpByEntityGroupIdQuery { EntityGroupId = entityGroupId }));
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            return GetResponse(await Mediator.Send(new GetEntityQuery { Id = id }));
        }
    }
}
