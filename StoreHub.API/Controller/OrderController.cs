using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using StoreHub.Application.Dtos.OrderDto;
using System.Security.Claims;

namespace StoreHub.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrder(OrderRequestDto order)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.orderService.CreateOrderAsync(order, userEmail);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResultDto>> GetOrderById(Guid id)
        {
            var data = await serviceManager.orderService.GetOrderByIdAsync(id);
            return Ok(data);
        }
        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrderByEmail()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var data = await serviceManager.orderService.GetOrderByEmailAsync(userEmail);
            return Ok(data);
        }

        [HttpGet("Delivery")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var data = await serviceManager.orderService.GetAllDeliveryMethod();
            return Ok(data);
        }
    }
}
