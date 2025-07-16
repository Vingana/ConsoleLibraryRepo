using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace WebApp.Extensions;

internal static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(o =>
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header using the Bearer scheme. Example: Bearer {your token}",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            };

            o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            };

            o.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}