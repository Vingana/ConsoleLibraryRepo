using WebApp.Services.CharacterServices;
using WebApp.Services.CharacterClassServices;
using WebApp.Services.RaceServices;
using WebApp.Services.FactionServices;
using WebApp.Services.AccountServices;

namespace WebApp.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICharacterService, CharacterService>();
        services.AddScoped<ICharacterClassService, CharacterClassService>();
        services.AddScoped<IRaceService, RaceService>();
        services.AddScoped<IFactionService, FactionService>();
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}