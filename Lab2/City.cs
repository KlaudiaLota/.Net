using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherForm.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Lat { get; set; }

        [Required]
        public double Lon { get; set; }

        // Relacja: jedno miasto może mieć wiele wpisów pogodowych
        public ICollection<WeatherData> WeatherData { get; set; }
    }
}
