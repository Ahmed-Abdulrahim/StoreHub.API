using AutoMapper;
using Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using StoreHub.Application.Services;
using StoreHub.Application.Services.Contracts;
using StoreHub.Application.Shared;
using StoreHub.Core.Contracts;
using StoreHub.Core.Models.Identity;


namespace Services
{
    public class ServiceManager(ICacheRepository cacheRepository, ICustomBasketRepository basketRepository,
        IUnitOfWork unitOfWork, IMapper map, UserManager<AppUser> user, IOptions<JwtOptions> jwtOptions, IConfiguration config) : IServiceManager
    {
        public IProductService IProductService => new ProductServices(unitOfWork, map);
        public ICustomBasketService basketService => new CustomBasketService(basketRepository, map);

        public ICacheService cacheService => new CacheService(cacheRepository);

        public IAuthService authService => new AuthService(map, user, jwtOptions);

        public IOrderService orderService => new OrderService(map, basketRepository, unitOfWork);

        public IpaymentService paymentService => new PaymentService(map, basketRepository, unitOfWork, config);
    }
}
