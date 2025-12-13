using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using StoreHub.Application.Dtos.AuthDto;
using StoreHub.Application.Dtos.OrderDto;
using System.Security.Claims;

namespace StoreHub.API.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto login)
        {
            var result = await serviceManager.authService.LoginAsync(login);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto resgister)
        {
            var result = await serviceManager.authService.RegisterAsync(resgister);
            return Ok(result);
        }


        [HttpGet("{email}")]
        public async Task<IActionResult> CheckEmailExist(string email)
        {
            var restult = await serviceManager.authService.CheckEmailExist(email);
            return Ok(restult);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.authService.GetCurrentUser(email);
            return Ok(result);
        }


        [HttpGet("GetAddress")]
        [Authorize]
        public async Task<ActionResult<ShippingAddressDto>> GetCurrentAddress()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.authService.GetCurrentUserAddress(userEmail);
            return Ok(result);
        }

        [HttpPost("updateAddress")]
        [Authorize]
        public async Task<ActionResult<ShippingAddressDto>> UpdateAddress(ShippingAddressDto model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.authService.UpdateCurrentUserAddress(model, userEmail);
            return Ok(result);
        }
    }
}
