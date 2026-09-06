using Market.Data;
using Market.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Market
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MarketDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("MarketConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<MarketDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<MarketDbContext>();
                Seeder.Seed(context);

                foreach (var product in context.Products)
                {
                    product.Rating = product.Rarity switch
                    {
                        "Contraband" => 5.0m,
                        "Covert" => 4.8m,
                        "Classified" => 4.6m,
                        "Restricted" => 4.3m,
                        "Mil-Spec" => 4.0m,
                        "Industrial Grade" => 3.7m,
                        "Consumer Grade" => 3.4m,
                        _ => 4.0m
                    };
                }

                context.SaveChanges();

                await IdentitySeeder.SeedAsync(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}