using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApp.Options;
using WebApp.Services.AuthServices;
using WebApp.Services.JwtServices;

namespace WebApp.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var key = Encoding.UTF8.GetBytes(configuration[$"{nameof(JwtOptions)}:Key"]!);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration[$"{nameof(JwtOptions)}:Issuer"],
                ValidAudience = configuration[$"{nameof(JwtOptions)}:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();

        return services;
    }
}