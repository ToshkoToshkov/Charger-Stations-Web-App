namespace Charger_Stations_Web_App.Data
{
    using Charger_Stations_Web_App.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;


    public class ChargerStationsDbContext : IdentityDbContext
    {
        public ChargerStationsDbContext(DbContextOptions<ChargerStationsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Charger> Chargers { get; init; }

        public DbSet<Category> Categories { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Charger>()
                .HasOne(c => c.Category)
                .WithMany(C => C.Chargers)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}