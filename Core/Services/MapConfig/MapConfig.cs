using AutoMapper;
using Domain.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MapConfig
{
    public class MapConfig:Profile
    {
        public MapConfig()
        {
            CreateMap<Product, ProductDto>().AfterMap((src, dest) => 
            {
                dest.BrandName = src.ProductBrands?.Name;
                dest.TypeName = src.ProductTypes?.Name;
            }).ReverseMap();

            CreateMap<ProductBrand, BrandDto>().ReverseMap();
            CreateMap<ProductType, TypeDto>().ReverseMap();
        }
    }
}
