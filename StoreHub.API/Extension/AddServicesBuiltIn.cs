using Domain.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace StoreHub.API.Extension
{
    public static class AddServicesBuiltIn
    {
        public static IServiceCollection AddServiceBuiltIn(this IServiceCollection service, IConfiguration configuration)
        {

            // Add services to the container.

            service.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();

            string txt = "";
            service.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            service.Configure<ApiBehaviorOptions>(config =>
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

            ValidationErrorResponseMethod(service);

            return service;

        }


        private static void ValidationErrorResponseMethod(IServiceCollection service)
        {
            service.Configure<ApiBehaviorOptions>(config =>
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

        }
    }


}

