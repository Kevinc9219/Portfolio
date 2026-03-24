using Microsoft.AspNetCore.Identity;
using LoginRollen.Models;

namespace LoginRollen.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = { "Admin", "Manager", "User" };
        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        await CreateUser(userManager, "admin@demo.com", "Admin123!", "Admin", "User", "Admin");
        await CreateUser(userManager, "manager@demo.com", "Manager123!", "Manager", "User", "Manager");
        await CreateUser(userManager, "user@demo.com", "User123!", "Regular", "User", "User");
    }

    private static async Task CreateUser(UserManager<ApplicationUser> um, string email, string password, string first, string last, string role)
    {
        if (await um.FindByEmailAsync(email) == null)
        {
            var user = new ApplicationUser { UserName = email, Email = email, FirstName = first, LastName = last, EmailConfirmed = true };
            var result = await um.CreateAsync(user, password);
            if (result.Succeeded) await um.AddToRoleAsync(user, role);
        }
    }
}
