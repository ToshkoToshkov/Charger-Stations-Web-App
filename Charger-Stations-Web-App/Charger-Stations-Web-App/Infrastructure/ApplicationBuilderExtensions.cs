namespace Charger_Stations_Web_App.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Charger_Stations_Web_App.Data.Models;
    using Charger_Stations_Web_App.Data;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ChargerStationsDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(ChargerStationsDbContext data)
        {
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
    }
}
