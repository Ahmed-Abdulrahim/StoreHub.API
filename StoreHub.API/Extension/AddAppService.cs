using Domain.Contracts;
using StoreHub.API.MiddelWare;

namespace StoreHub.API.Extension
{
    public static class AddAppService
    {
        public static async Task<WebApplication> BuildApp(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorMiddleware>();
            using var scope = app.Services.CreateScope();
            var initialize = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initialize.Initializer();
            await initialize.IdentityInitializer();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
    }
}
