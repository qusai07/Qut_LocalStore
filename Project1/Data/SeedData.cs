using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Data
{
    public static class SeedData
    {
        public static async Task InitializeUser(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (await userManager.Users.AnyAsync())
                return;
            var roles = new List<IdentityRole<int>>()
              {
              new IdentityRole<int>{Name="Admin"},
              new IdentityRole<int>{Name="User"},
              };
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
            var Admin = new User()
            {
                UserName = "admin_tps2024",
                Email = "admintps2024@gmail.com",
                PhoneNumber = "0776924478",
            };
            await userManager.CreateAsync(Admin, "Pa$$w0rd");
            await userManager.AddToRoleAsync(Admin, "Admin");

         


            }
    }
}
