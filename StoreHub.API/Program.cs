
using Domain.Contracts;
using Domain.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data;
using Services;
using Services.Abstraction;
using Services.MapConfig;
using StoreHub.API.MiddelWare;
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
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(typeof(MapConfig));
            string txt = "";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            #region Handel ValidationErrors

            builder.Services.Configure<ApiBehaviorOptions>(config =>
    {
        config.InvalidModelStateResponseFactory = (actioResuilt) =>
        {
            actioResuilt.ModelState.Where(m => m.Value.Errors.Any()).Select(m => new
            {
                m.Key,
                m.Value.Errors
            });
            var errors = actioResuilt.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .Select(e => new ValidationError()
                {
                    Field = e.Key,
                    Message = e.Value.Errors.Select(er => er.ErrorMessage)
                });
            var response = new ValidationErrorResponse
            {
                Errors = errors
            };
            return new BadRequestObjectResult(response);
        };

    });

            #endregion

            var app = builder.Build();

            app.UseMiddleware<GlobalErrorMiddleware>();
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
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
