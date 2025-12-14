using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using StoreHub.Application.Dtos.OrderDto;
using StoreHub.Application.Services.Contracts;
using StoreHub.Application.Speicifications;
using StoreHub.Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Services
{
    public class OrderService(IMapper mapper, ICustomBasketRepository basketRepository, IUnitOfWork unit) : IOrderService
    {

        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderData, string userEmail)
        {
            var Address = mapper.Map<ShippingAddress>(orderData.Address);
            var basket = await basketRepository.GetBasketAsync(orderData.BasketId);
            if (basket is null) throw new Exception("Basket not found");
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unit.GetGenericRepo<Product, int>().GetById(item.Id);
                if (product is null) throw new Exception($"Product with id {item.Id} not found");
                var orderItem = new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), item.Quantity, product.Price);
                orderItems.Add(orderItem);
            }
            var deliverMethod = await unit.GetGenericRepo<DeliveryMethod, int>().GetById(orderData.DeliveryMethodId);
            if (deliverMethod is null) throw new Exception("Delivery method not found");
            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);

            var spec = new OrderPaymentIntentIdSpecification(basket.PaymentIntentId);
            var existIntenId = await unit.GetGenericRepo<Order, Guid>().GetByIdWithSpec(spec);
            if (existIntenId is not null)
            {
                unit.GetGenericRepo<Order, Guid>().Delete(existIntenId);
            }
            var order = new Order(userEmail, Address, orderItems, deliverMethod, basket.PaymentIntentId, subTotal);
            await unit.GetGenericRepo<Order, Guid>().AddAsync(order);
            var rowAffected = await unit.SaveChangeAsync();
            var result = mapper.Map<OrderResultDto>(order);
            return rowAffected > 0 ? result : throw new Exception("Problem creating order");
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);
            var order = await unit.GetGenericRepo<Order, Guid>().GetByIdWithSpec(spec);
            if (order is null) throw new Exception("Order Not Found");
            var result = mapper.Map<OrderResultDto>(order);
            return result;
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethod()
        {
            var deliverymethods = await unit.GetGenericRepo<DeliveryMethod, int>().GetAll();
            if (!deliverymethods.Any()) throw new Exception("No Delivery Methods Found");
            var data = mapper.Map<IEnumerable<DeliveryMethodDto>>(deliverymethods);
            return data;
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrderByEmailAsync(string userEmail)
        {
            var spec = new OrderSpecification(userEmail);
            var orders = await unit.GetGenericRepo<Order, Guid>().GetAllWithSpec(spec);
            if (!orders.Any()) throw new Exception("No Orders Found");
            var result = mapper.Map<IEnumerable<OrderResultDto>>(orders);
            return result;
        }
    }
}
