
using Domain.Contracts;
using Domain.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data;
using Services;
using Services.Abstraction;
using Services.MapConfig;
using StoreHub.API.Extension;
using StoreHub.API.MiddelWare;
using System.Threading.Tasks;

namespace StoreHub.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add DpendencyInjection
            builder.Services.AddServiceDpendencyInjection(builder.Configuration);

            // Add servicesFor Auto Mapping.
            builder.Services.AddAutoMapping();


            builder.Services.AddServiceBuiltIn(builder.Configuration);


            var app = builder.Build();

            await app.BuildApp();

            app.Run();
        }
    }
}
