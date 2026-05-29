using BTVN6.Models;
using Microsoft.AspNetCore.Identity;

namespace BTVN6.Data;

public static class DbInitializer
{
    public const string AdminRole = "Admin";
    public const string MemberRole = "Member";

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var roleName in new[] { AdminRole, MemberRole })
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        await EnsureUserAsync(userManager, "admin", "admin@qlbanhang.local", "Quản trị viên", "Hà Nội", "Admin@123", AdminRole);
        await EnsureUserAsync(userManager, "member", "member@qlbanhang.local", "Thành viên", "TP.HCM", "Member@123", MemberRole);
    }

    private static async Task EnsureUserAsync(
        UserManager<ApplicationUser> userManager,
        string userName,
        string email,
        string fullName,
        string address,
        string password,
        string role)
    {
        var existing = await userManager.FindByNameAsync(userName);
        if (existing != null)
        {
            if (!await userManager.IsInRoleAsync(existing, role))
            {
                await userManager.AddToRoleAsync(existing, role);
            }
            return;
        }

        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email,
            EmailConfirmed = true,
            FullName = fullName,
            Address = address
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
