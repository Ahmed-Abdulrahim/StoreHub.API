using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;


namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper map) : IServiceManager
    {
        public IProductService IProductService => new ProductServices(unitOfWork, map);
    }
}
