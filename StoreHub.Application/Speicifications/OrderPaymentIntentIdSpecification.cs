using Services.Speicifications;
using StoreHub.Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Speicifications
{
    public class OrderPaymentIntentIdSpecification : BaseSpeicification<Order, Guid>
    {
        public OrderPaymentIntentIdSpecification(string PaymentId) : base(o => o.PaymentIntendId == PaymentId)
        {
            AddInclude.Add(o => o.DeliveryMethod);
            AddInclude.Add(o => o.OrderItem);
        }
    }
}
