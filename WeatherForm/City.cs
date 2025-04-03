using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherForm.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required double Lat { get; set; }
        public required double Lon { get; set; }
        public int? Timezone { get; set; }

        // Relacja: jedno miasto może mieć wiele wpisów pogodowych
        public ICollection<WeatherData> WeatherData { get; set; }
    }
}
