using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data;
using Services;
using Services.Abstraction;

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

            return services;
        }
    }
}
