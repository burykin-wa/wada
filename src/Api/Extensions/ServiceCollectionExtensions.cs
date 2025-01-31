using Api.Handlers;
using Api.Models;
using Core.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using PrimeNG.TableFilter.Models;

namespace Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExceptionHandler<ExceptionHandler>();

            services.AddProblemDetails();

            services.AddServices();

            services.AddBearerAuthentication();

            services.AddAuthorization();

            services.AddDbContext(configuration.GetConnectionString("DbConnection")!);

            return services;
        }

        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.EnableAnnotations();
                cfg.IgnoreObsoleteProperties();
                cfg.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");
                cfg.CustomSchemaIds(type => type == typeof(TableFilterModel) ? "PrimeTableFilterModel" : type.Name);
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = configuration["Title"],
                    Version = configuration["API_VERSION"],
                    Contact = new OpenApiContact
                    {
                        Name = configuration["Organization"],
                        Url = new Uri(configuration["ContactUri"]!)
                    },
                });

                cfg.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "Put your token (without [bearer] prefix) here",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Name = "Authorization"
                });

                // Make sure swagger UI requires a Bearer token specified
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };

                cfg.AddSecurityRequirement(securityRequirements);
            });
        }
    }
}
