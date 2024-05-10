using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GameReviewSite.DAL.Context;

namespace GameReviewSite.UI.Services
{
    public class IdentityHostingStartup
    {
        public static void ConfigureIdentityServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<GameDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefConn")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<GameDbContext>();

            // Burada diğer identity konfigürasyonlarını ve rol yönetimi işlemlerini ekleyebilirsiniz
        }

        public static async Task InitializeRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Admin kullanıcısını oluşturma
            var admin = new IdentityUser { UserName = "masteradmin", Email = "masteradmin@master.com" };
            string adminPassword = "Q1w2e3r4!";
            var adminUser = await userManager.FindByEmailAsync(admin.Email);
            if (adminUser == null)
            {
                var createAdmin = await userManager.CreateAsync(admin, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
