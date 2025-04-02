using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForm.Models
{
    public class WeatherData
    {
        [Key]
        public int Id { get; set; }

        // Relacja do tabeli City
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        [Required]
        public double Temperature { get; set; }

        [Required]
        public int Humidity { get; set; }

        [Required]
        public int Pressure { get; set; }

        [Required]
        public string WeatherDescription { get; set; }

        [Required]
        public double WindSpeed { get; set; }

        [Required]
        public long Sunrise { get; set; }

        [Required]
        public long Sunset { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } // Czas pobrania danych pogodowych
    }
}
