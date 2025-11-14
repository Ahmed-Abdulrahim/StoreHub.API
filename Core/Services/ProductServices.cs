using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Services.Speicifications;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices(IUnitOfWork unitOfWork, IMapper map) : IProductService
    {

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Brands = await unitOfWork.GetGenericRepo<ProductBrand, int>().GetAll();
            var models = map.Map<IEnumerable<BrandDto>>(Brands);
            return models;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync(int? brandId, int? typeId, string? sort, int? pageIndex, int? pageSize)
        {
            var spec = new ProductSpecification(brandId, typeId, sort, pageIndex, pageSize);
            var products = await unitOfWork.GetGenericRepo<Product, int>().GetAllWithSpec(spec);
            var models = map.Map<IEnumerable<ProductDto>>(products);
            return models;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypes()
        {
            var types = await unitOfWork.GetGenericRepo<ProductType, int>().GetAll();
            var models = map.Map<IEnumerable<TypeDto>>(types);
            return models;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecification(id);
            var product = await unitOfWork.GetGenericRepo<Product, int>().GetByIdWithSpec(spec);
            var model = map.Map<ProductDto>(product);
            return model;
        }
    }
}
