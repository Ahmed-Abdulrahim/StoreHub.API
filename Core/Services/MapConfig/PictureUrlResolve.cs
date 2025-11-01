using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MapConfig
{
    public class PictureUrlResolve (IConfiguration configration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;
            return $"{configration["BaseUrl"]}/{source.PictureUrl}";
        }
    }
}
