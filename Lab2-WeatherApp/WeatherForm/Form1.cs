using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WeatherForm.Models;

namespace WeatherForm
{
    public partial class Form1 : Form
    {
        public HttpClient client;
        private AppDbContext db;
        public Form1()
        {
            InitializeComponent();
            db = new AppDbContext();
            client = new HttpClient();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cityName = textBox3.Text.Trim();
                bool hasCoordinates = double.TryParse(textBox1.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude) &&
                                      double.TryParse(textBox2.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude);

                City city = null;
                WeatherData weatherData = null;

                if (!string.IsNullOrEmpty(cityName))
                {
                    city = db.Cities.Include(c => c.WeatherData).FirstOrDefault(c => c.Name.ToLower() == cityName.ToLower());
                    weatherData = city?.WeatherData.OrderByDescending(w => w.Id).FirstOrDefault();
                }

                if (city != null && weatherData != null)
                {
                    DisplayWeather(weatherData);
                    return;
                }

                if (hasCoordinates)
                {
                    string apiKey = "6115f0cc1473a569cf89607b7072e0dd";
                    string call = $"https://api.openweathermap.org/data/2.5/weather?lat={textBox1.Text}&lon={textBox2.Text}&appid={apiKey}&units=metric";

                    string response = await client.GetStringAsync(call);
                    var weatherInfo = JsonSerializer.Deserialize<ToDo>(response);

                    if (weatherInfo == null || weatherInfo.coord == null)
                    {
                        MessageBox.Show("Podane miasto nie istnieje w bazie ani w API.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    city = new City
                    {
                        Name = weatherInfo.name,
                        Lat = weatherInfo.coord.lat,
                        Lon = weatherInfo.coord.lon,
                        Timezone = weatherInfo.timezone
                    };
                    db.Cities.Add(city);
                    db.SaveChanges();

                    weatherData = new WeatherData
                    {
                        CityId = city.Id,
                        Temperature = weatherInfo.main.temp,
                        Humidity = weatherInfo.main.humidity,
                        Pressure = weatherInfo.main.pressure,
                        WeatherDescription = weatherInfo.weather.FirstOrDefault()?.description ?? "Brak danych",
                        WindSpeed = weatherInfo.wind.speed,
                        Sunrise = weatherInfo.sys.sunrise,
                        Sunset = weatherInfo.sys.sunset
                    };

                    db.WeatherRecords.Add(weatherData);
                    db.SaveChanges();

                    DisplayWeather(weatherData);
                }
                else
                {
                    MessageBox.Show("Miasto nie zostało znalezione w bazie, a brak współrzędnych do API.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd pobierania danych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayWeather(WeatherData weather)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add($"City: {weather.City.Name}");
            listBox1.Items.Add($"Coordinates: Lat={weather.City.Lat}, Lon={weather.City.Lon}");
            listBox1.Items.Add($"Timezone: {weather.City.Timezone}");
            listBox1.Items.Add($"Temperature: {weather.Temperature}°C");
            listBox1.Items.Add($"Humidity: {weather.Humidity}%");
            listBox1.Items.Add($"Pressure: {weather.Pressure} hPa");
            listBox1.Items.Add($"Weather: {weather.WeatherDescription}");
            listBox1.Items.Add($"Wind Speed: {weather.WindSpeed} m/s");
            listBox1.Items.Add($"Sunrise: {DateTimeOffset.FromUnixTimeSeconds(weather.Sunrise).ToLocalTime()}");
            listBox1.Items.Add($"Sunset: {DateTimeOffset.FromUnixTimeSeconds(weather.Sunset).ToLocalTime()}");
        }
    }

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
