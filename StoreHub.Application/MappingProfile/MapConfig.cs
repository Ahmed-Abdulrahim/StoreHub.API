using AutoMapper;
using Domain.Models;
using Shared.Dtos;


namespace Services.MapConfig
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<Product, ProductDto>().AfterMap((src, dest) =>
            {
                dest.BrandName = src.ProductBrands?.Name;
                dest.TypeName = src.ProductTypes?.Name;
            }).ForMember(p => p.PictureUrl, o => o.MapFrom<PictureUrlResolve>()).ReverseMap();

            CreateMap<ProductBrand, BrandDto>().ReverseMap();
            CreateMap<ProductType, TypeDto>().ReverseMap();
        }
    }
}
