
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data;
using System.Threading.Tasks;

namespace StoreHub.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreHubDbContext>(op =>
            op.UseSqlServer(builder.Configuration.GetConnectionString("conn1"))
            );
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();

            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            var initialize = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initialize.Initializer();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
