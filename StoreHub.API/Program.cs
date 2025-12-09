using StoreHub.API.Extension;


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
