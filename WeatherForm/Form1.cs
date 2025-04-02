using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherForm.Models;

namespace WeatherForm
{
    public partial class Form1 : Form
    {
        public HttpClient client;

        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        // Metoda ładowania miast do ComboBox
        private void LoadCities()
        {
            using (var db = new AppDbContext())
            {
                var cities = db.Cities.ToList();
                comboBoxCities.Items.Clear();
                foreach (var city in cities)
                {
                    comboBoxCities.Items.Add(city.Name);  // Załóżmy, że masz tabelę z nazwami miast
                }
            }
        }

        // Obsługa kliknięcia przycisku "Pobierz pogodę"
        private async void button1_Click(object sender, EventArgs e)
        {
            string selectedCity = comboBoxCities.SelectedItem?.ToString();
            string lat = textBox1.Text;
            string lon = textBox2.Text;

            // Jeśli miasto zostało wybrane z ComboBox
            if (!string.IsNullOrEmpty(selectedCity))
            {
                using (var db = new AppDbContext())
                {
                    var city = db.Cities.FirstOrDefault(c => c.Name == selectedCity);
                    if (city != null)
                    {
                        DisplayWeather(city.WeatherData.OrderByDescending(w => w.Id).First());  // Zakładając, że masz relację z WeatherData w City
                        return;
                    }
                }
            }

            // Jeśli współrzędne zostały wprowadzone, spróbuj pobrać dane z API
            if (string.IsNullOrWhiteSpace(lat) || string.IsNullOrWhiteSpace(lon))
            {
                MessageBox.Show("Proszę podać współrzędne (szerokość i długość geograficzną).", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                double latitude = double.Parse(lat, CultureInfo.InvariantCulture);
                double longitude = double.Parse(lon, CultureInfo.InvariantCulture);

                string apiKey = "6115f0cc1473a569cf89607b7072e0dd";
                string call = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}";

                string response = await client.GetStringAsync(call);
                ToDo t = JsonSerializer.Deserialize<ToDo>(response);

                using (var db = new AppDbContext())
                {
                    var weatherData = new WeatherData
                    {
                        City = t.name,
                        Timezone = t.timezone,
                        Lat = t.coord.lat,
                        Lon = t.coord.lon,
                        Temperature = t.main.temp - 273.15,
                        Humidity = t.main.humidity,
                        Pressure = t.main.pressure,
                        WeatherDescription = t.weather[0].description,
                        WindSpeed = t.wind.speed,
                        Sunrise = t.sys.sunrise,
                        Sunset = t.sys.sunset
                    };

                    // Zapisz dane pogodowe w bazie
                    db.WeatherRecords.Add(weatherData);
                    db.SaveChanges();
                }
                DisplayWeather(t);
            }
            catch (FormatException)
            {
                MessageBox.Show("Współrzędne muszą być liczbami zmiennoprzecinkowymi, oddzielonymi kropką (np. 52.2298).", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd pobierania danych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metoda wyświetlania danych pogodowych
        private void DisplayWeather(WeatherData weather)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add($"City: {weather.City}");
            listBox1.Items.Add($"Timezone: {weather.Timezone}");
            listBox1.Items.Add($"Coordinates: Lat={weather.Lat}, Lon={weather.Lon}");
            listBox1.Items.Add($"Temperature: {weather.Temperature}°C");
            listBox1.Items.Add($"Humidity: {weather.Humidity}%");
            listBox1.Items.Add($"Pressure: {weather.Pressure} hPa");
            listBox1.Items.Add($"Weather: {weather.WeatherDescription}");
            listBox1.Items.Add($"Wind Speed: {weather.WindSpeed} m/s");
            listBox1.Items.Add($"Sunrise: {DateTimeOffset.FromUnixTimeSeconds(weather.Sunrise).ToLocalTime()}");
            listBox1.Items.Add($"Sunset: {DateTimeOffset.FromUnixTimeSeconds(weather.Sunset).ToLocalTime()}");
        }

        // Załóżmy, że ComboBox jest wczytywany przy starcie aplikacji
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCities();
        }
    }

    // Klasy do deserializacji danych z API OpenWeather
    internal class ToDo
    {
        public string name { get; set; }
        public int timezone { get; set; }
        public Coord coord { get; set; }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Wind wind { get; set; }
        public Sys sys { get; set; }
    }

    internal class Coord
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    internal class Weather
    {
        public string description { get; set; }
    }

    internal class Wind
    {
        public double speed { get; set; }
    }

    internal class Sys
    {
        public long sunrise { get; set; }
        public long sunset { get; set; }
    }

    internal class Main
    {
        public double temp { get; set; }
        public int humidity { get; set; }
        public int pressure { get; set; }
    }
}
