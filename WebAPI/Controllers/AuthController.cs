using Business.Handlers.Users.Commands;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("changeuserpassword")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] UserChangePasswordCommand command)
        {
            return GetResponse(await Mediator.Send(command));
        }

        [HttpPost("test")]
        public IActionResult LoginTest()
        {
            var auth = Request.Headers["Authorization"];
            var token = new JwtHelper(_configuration).DecodeToken(auth);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand command)
        {
            return GetResponse(await Mediator.Send(command));
        }

        [AllowAnonymous]
        [HttpPost("loginwithrefreshtoken")]
        public async Task<IActionResult> LoginWithRefreshToken([FromBody] UserLoginWithRefreshTokenCommand command)
        {
            return GetResponse(await Mediator.Send(command));
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordCommand command)
        {
            return GetResponse(await Mediator.Send(command));
        }
    }
}