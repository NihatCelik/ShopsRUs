using Business.Handlers.Discounts.Commands;
using Business.Handlers.Discounts.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : BaseApiController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await Mediator.Send(new GetDiscountsQuery()));
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            return GetResponse(await Mediator.Send(new GetDiscountQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateDiscountCommand createDiscount)
        {
            return GetResponse(await Mediator.Send(createDiscount));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDiscountCommand updateDiscount)
        {
            return GetResponse(await Mediator.Send(updateDiscount));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteDiscountCommand deleteDiscount)
        {
            return GetResponse(await Mediator.Send(deleteDiscount));
        }
    }
}
