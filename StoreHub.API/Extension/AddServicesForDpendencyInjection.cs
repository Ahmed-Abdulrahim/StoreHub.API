using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data;
using Services;
using Services.Abstraction;
using StackExchange.Redis;
using StoreHub.Application.Services;
using StoreHub.Application.Services.Contracts;
using StoreHub.Infrastructure.Repository;

namespace StoreHub.API.Extension
{
    public static class AddServicesForDpendencyInjection
    {
        public static IServiceCollection AddServiceDpendencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreHubDbContext>(op =>
               op.UseSqlServer(configuration.GetConnectionString("conn1")));
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ICustomBasketService, CustomBasketService>();
            services.AddScoped<ICustomBasketRepository, CustomBasketRepository>();
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            });

            return services;
        }
    }
}
