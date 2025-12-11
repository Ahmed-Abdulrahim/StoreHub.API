using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using StoreHub.API.Errors;
using StoreHub.Application.Dtos.CustomBasket;

namespace StoreHub.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController(IServiceManager servicesManager) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomBasketDto>> GetBasket(string id)
        {
            var result = await servicesManager.basketService.GetBasketAsync(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<CustomBasketDto>> UpdateBasket(CustomBasketDto basket)
        {
            var result = await servicesManager.basketService.UpdateBasketAsync(basket);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await servicesManager.basketService.DeleteBasketAsync(id);
            return Ok(new ApiResponse(200, "Deleted Success"));
        }

    }
}
