using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;
using StoreHub.Application.Services;
using StoreHub.Application.Services.Contracts;
using StoreHub.Core.Contracts;


namespace Services
{
    public class ServiceManager(ICacheRepository cacheRepository, ICustomBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper map) : IServiceManager
    {
        public IProductService IProductService => new ProductServices(unitOfWork, map);
        public ICustomBasketService basketService => new CustomBasketService(basketRepository, map);

        public ICacheService cacheService => new CacheService(cacheRepository);
    }
}
