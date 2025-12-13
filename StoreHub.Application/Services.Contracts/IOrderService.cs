using StoreHub.Application.Dtos.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Services.Contracts
{
    public interface IOrderService
    {
        Task<OrderResultDto> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderResultDto>> GetOrderByEmailAsync(string userEmail);
        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethod();
        Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderData, string userEmail);
    }
}
