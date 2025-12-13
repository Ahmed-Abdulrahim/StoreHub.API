using Services.Speicifications;
using StoreHub.Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Speicifications
{
    public class OrderSpecification : BaseSpeicification<Order, Guid>
    {
        public OrderSpecification(Guid id) : base(o => o.Id == id)
        {
            AddInclude.Add(o => o.DeliveryMethod);
            AddInclude.Add(o => o.OrderItem);
        }
        public OrderSpecification(string userEmail) : base(o => o.UserEmail == userEmail)
        {
            AddInclude.Add(o => o.DeliveryMethod);
            AddInclude.Add(o => o.OrderItem);
            ApplyOrderBy(o => o.OrderDate);
        }
    }
}
