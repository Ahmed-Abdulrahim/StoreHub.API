using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Core.Models.Orders
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {

        }
        public Order(string userEmail, ShippingAddress address, ICollection<OrderItem> orderItem, DeliveryMethod deliveryMethod, string paymentIntendId, decimal subTotal)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            Address = address;
            OrderItem = orderItem;
            DeliveryMethod = deliveryMethod;
            PaymentIntendId = paymentIntendId;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; }
        public ShippingAddress Address { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public string PaymentIntendId { get; set; }
        public decimal SubTotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    }
}
