using Microsoft.AspNetCore.Identity;

namespace WebApp.Extensions;

public static class SeedExtensions
{
    public static async Task SeedRolesAsync(this IServiceScope scope)
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        await RoleInitializer.SeedRolesAsync(roleManager);
    }

    public static async Task SeedAdminUserAsync(this IServiceScope scope)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        await RoleInitializer.SeedAdminUserAsync(userManager);
    }
}