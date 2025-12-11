using AutoMapper;
using Domain.Models;
using StoreHub.Application.Dtos.CustomBasket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.MappingProfile
{
    public class CustomBasketProfile : Profile
    {

        public CustomBasketProfile()
        {
            CreateMap<CustomBasket, CustomBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        }
    }
}
