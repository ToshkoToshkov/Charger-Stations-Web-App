namespace Charger_Stations_Web_App.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Charger_Stations_Web_App.Data.Models;
    using Charger_Stations_Web_App.Data;
    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ChargerStationsDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {

            var data = services.GetRequiredService<ChargerStationsDbContext>();


            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "Monophase"},
                new Category {Name = "Three-phase"},
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };
                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@chrs.com";
                    const string adminPassword = "admin123";

                    var user = new IdentityUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                    };

                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
