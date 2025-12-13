using StoreHub.Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Dtos.OrderDto
{
    public class OrderResultDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public ShippingAddressDto Address { get; set; }
        public ICollection<OrderItemDto> OrderItem { get; set; }
        public string DeliveryMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentIntendId { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    }
}
