using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Dtos.OrderDto
{
    public class OrderRequestDto
    {
        public string BasketId { get; set; }
        public ShippingAddressDto Address { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
