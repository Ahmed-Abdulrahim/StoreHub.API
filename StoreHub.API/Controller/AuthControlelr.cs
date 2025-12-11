using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using StoreHub.Application.Dtos.AuthDto;

namespace StoreHub.API.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthControlelr(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("/login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto login)
        {
            var result = await serviceManager.authService.LoginAsync(login);
            return Ok(result);
        }

        [HttpPost("/register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto resgister)
        {
            var result = await serviceManager.authService.RegisterAsync(resgister);
            return Ok(result);
        }
    }
}
