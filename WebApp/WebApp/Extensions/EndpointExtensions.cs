using Microsoft.AspNetCore.Mvc;
using WebApp.DTO.Auth;
using WebApp.Services.AuthServices;

namespace WebApp.Extensions;

public static class EndpointExtensions
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost(RouteNames.SignUp,
            async ([FromServices] IAuthService authService, [FromBody] RegisterDto model) =>
            {
                return await authService.RegisterUserAsync(model);
            });

        app.MapPost(RouteNames.SignIn,
            async ([FromServices] IAuthService authService, [FromBody] LoginDto model) =>
            {
                return await authService.LoginUserAsync(model);
            });
    }
}