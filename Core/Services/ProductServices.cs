using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices(IUnitOfWork unitOfWork , IMapper map) : IProductService
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IMapper map = map;

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Brands = await unitOfWork.GetGenericRepo<ProductBrand, int>().GetAll();
            var models = map.Map<IEnumerable<BrandDto>>(Brands);
            return models;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            var products = await unitOfWork.GetGenericRepo<Product, int>().GetAll();
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
            var product = await unitOfWork.GetGenericRepo<Product, int>().GetById(id);
            var model = map.Map<ProductDto>(product);
            return model;
        }
    }
}
