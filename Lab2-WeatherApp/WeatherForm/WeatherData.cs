using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForm.Models
{
    public class WeatherData
    {
        [Key]
        public int Id { get; set; }
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public required double Temperature { get; set; }
        public required int Humidity { get; set; }
        public required int Pressure { get; set; }
        public required string WeatherDescription { get; set; }
        public required double WindSpeed { get; set; }
        public required long Sunrise { get; set; }
        public required long Sunset { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } 
    }
}
