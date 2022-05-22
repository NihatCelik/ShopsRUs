using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await Mediator.Send(new GetUsersQuery()));
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            return GetResponse(await Mediator.Send(new GetUserQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand createUser)
        {
            return GetResponse(await Mediator.Send(createUser));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUser)
        {
            return GetResponse(await Mediator.Send(updateUser));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUser)
        {
            return GetResponse(await Mediator.Send(deleteUser));
        }
    }
}
