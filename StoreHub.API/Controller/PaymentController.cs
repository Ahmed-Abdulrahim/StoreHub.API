using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using StoreHub.Application.Dtos.CustomBasket;

namespace StoreHub.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController(IServiceManager servicesManager) : ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomBasketDto>> CreatePaymentLink(string basketId)
        {
            var result = await servicesManager.paymentService.CreatePaymentIntentAsync(basketId);
            if (result is null) return BadRequest("Problem with your basket");
            return Ok(result);
        }
    }
}
