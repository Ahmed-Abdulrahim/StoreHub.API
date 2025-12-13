using Services.MapConfig;
using StoreHub.Application.MappingProfile;

namespace StoreHub.API.Extension
{
    public static class AddServiceAutoMapping
    {
        public static IServiceCollection AddAutoMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapConfig));
            services.AddAutoMapper(typeof(CustomBasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));

            return services;
        }
    }
}
