using Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data;
using Services;
using Services.Abstraction;
using StackExchange.Redis;
using StoreHub.Application.Services;
using StoreHub.Application.Services.Contracts;
using StoreHub.Core.Contracts;
using StoreHub.Core.Models.Identity;
using StoreHub.Infrastructure.Identity;
using StoreHub.Infrastructure.Repository;

namespace StoreHub.API.Extension
{
    public static class AddServicesForDpendencyInjection
    {
        public static IServiceCollection AddServiceDpendencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreHubDbContext>(op =>
               op.UseSqlServer(configuration.GetConnectionString("conn1")));
            services.AddDbContext<StoreHubIdentityDbContext>(op =>
               op.UseSqlServer(configuration.GetConnectionString("conn2")));

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<StoreHubIdentityDbContext>();

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ICustomBasketService, CustomBasketService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ICustomBasketRepository, CustomBasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            });

            return services;
        }
    }
}
