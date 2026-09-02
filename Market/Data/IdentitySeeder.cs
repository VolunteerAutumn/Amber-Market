using Microsoft.AspNetCore.Identity;

namespace Market.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create roles
            string[] roles = { "User", "Admin" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create normal user
            var user = await userManager.FindByNameAsync("user");

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "user",
                    Email = "user@ambermarket.local",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "qwerp123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }

            // Create admin
            var admin = await userManager.FindByNameAsync("admin");

            if (admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@ambermarket.local",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "adminqwerp123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}