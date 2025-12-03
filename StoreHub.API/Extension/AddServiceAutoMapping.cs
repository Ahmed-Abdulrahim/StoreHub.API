using Services.MapConfig;

namespace StoreHub.API.Extension
{
    public static class AddServiceAutoMapping
    {
        public static IServiceCollection AddAutoMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapConfig));

            return services;
        }
    }
}
