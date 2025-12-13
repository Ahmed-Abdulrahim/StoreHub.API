using StoreHub.Application.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
        public IProductService IProductService { get; }
        public ICustomBasketService basketService { get; }
        public ICacheService cacheService { get; }
        public IAuthService authService { get; }
        public IOrderService orderService { get; }
    }
}
