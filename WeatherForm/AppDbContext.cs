using Microsoft.EntityFrameworkCore;

namespace WeatherForm.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherData> WeatherRecords { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=weather.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacja jeden-do-wielu: Miasto → Dane pogodowe
            modelBuilder.Entity<City>()
                .HasMany(c => c.WeatherData)
                .WithOne(w => w.City)
                .HasForeignKey(w => w.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            // Klucz unikalny dla nazwy miasta (żeby nie było duplikatów)
            modelBuilder.Entity<City>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }

}
