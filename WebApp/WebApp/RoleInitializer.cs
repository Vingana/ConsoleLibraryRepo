using Microsoft.AspNetCore.Identity;

namespace WebApp;

public static class RoleInitializer
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
    {
        var roles = new[] { "User", "Admin" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(role));
            }
        }
    }

    public static async Task SeedAdminUserAsync(UserManager<User> userManager)
    {
        string adminUserName = "admin";
        string adminEmail = "admin@example.com";
        string adminPassword = "Admino_nymous228";

        var adminUser = await userManager.FindByNameAsync(adminUserName);

        if (adminUser == null)
        {
            adminUser = new User { UserName = adminUserName, Email = adminEmail };
            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}