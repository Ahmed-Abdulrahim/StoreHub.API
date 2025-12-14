using StoreHub.Application.Dtos.CustomBasket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Services.Contracts
{
    public interface IpaymentService
    {
        Task<CustomBasketDto> CreatePaymentIntentAsync(string basketId);
    }
}
