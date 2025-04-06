using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WeatherForm.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherData> WeatherRecords { get; set; }

        private static string dbPath;

        public AppDbContext()
        {
            if (string.IsNullOrEmpty(dbPath))
            {
                string folder = FileSystem.AppDataDirectory;
                dbPath = Path.Combine(folder, "weather.db");
            }

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Filename={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(c => c.WeatherData)
                .WithOne(w => w.City)
                .HasForeignKey(w => w.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<City>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
