using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StoreHub.API.Errors;
using StoreHub.Application.Shared;
using System.Text;

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

            string txt = "AddPolicy";
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

            service.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOptions.issuer,
                    ValidAudience = jwtOptions.audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.secretKey))
                };
            });


            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {your JWT token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });


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

