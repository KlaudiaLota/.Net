using System;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging.Abstractions;
using WeatherForm.Models;

namespace MauiWeatherApp
{
    public partial class MainPage : ContentPage
    {
        private AppDbContext db;
        private HttpClient client;

        public MainPage()
        {
            InitializeComponent();
            db = new AppDbContext();
            client = new HttpClient();
            LoadCityList();
        }
        private async void OnGetWeatherClicked(object sender, EventArgs e)
        {
            try
            {
                double latitude = 0, longitude = 0;
                bool hasCoordinates = double.TryParse(latEntry.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out latitude) &&
                                      double.TryParse(lonEntry.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out longitude);

                City city = null;
                WeatherData weatherData = null;
                string cityName = cityEntry.Text?.Trim();

                if (!string.IsNullOrEmpty(cityName))
                {
                    city = db.Cities.Include(c => c.WeatherData).FirstOrDefault(c => c.Name.ToLower() == cityName.ToLower());
                    weatherData = db.WeatherRecords.Where(w => w.CityId == city.Id).OrderByDescending(w => w.Id).FirstOrDefault();
                    if (weatherData != null)
                    {
                        DisplayWeather(weatherData);
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Błąd", "Nie znaleziono danych pogodowych dla podanego miasta.", "OK");
                        return;
                    }
                }

                if (city == null && cityPicker.SelectedItem is City selectedFromPicker)
                {
                    city = db.Cities.Include(c => c.WeatherData).FirstOrDefault(c => c.Id == selectedFromPicker.Id);
                    weatherData = db.WeatherRecords.Where(w => w.CityId == city.Id).OrderByDescending(w => w.Id).FirstOrDefault();

                    if (weatherData != null)
                    {
                        DisplayWeather(weatherData);
                        return;
                    }
                }

                if (city == null && hasCoordinates)
                {
                    string apiKey = "6115f0cc1473a569cf89607b7072e0dd";
                    string call = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";

                    string response = await client.GetStringAsync(call);
                    var weatherInfo = JsonSerializer.Deserialize<ToDo>(response);

                    if (weatherInfo == null || weatherInfo.coord == null)
                    {
                        await DisplayAlert("Błąd", "Podane miasto nie istnieje w bazie ani w API.", "OK");
                        return;
                    }

                    city = new City
                    {
                        Name = weatherInfo.name,
                        Lat = weatherInfo.coord.lat,
                        Lon = weatherInfo.coord.lon,
                        Timezone = weatherInfo.timezone
                    };

                    if (string.IsNullOrEmpty(city.Name))
                    {
                        await DisplayAlert("Błąd", "Miasto nie ma nazwy!", "OK");
                        return;
                    }
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
                    LoadCityList();
                }
                else
                {
                    await DisplayAlert("Błąd", "Miasto nie zostało znalezione w bazie, a brak współrzędnych do API.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", $"Błąd pobierania danych: {ex.Message}", "OK");
            }
        }
        private void LoadCityList()
        {
            var cities = db.Cities.ToList();
            cityPicker.ItemsSource = cities;
        }
        private void ValidateNumericEntry(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            string input = entry?.Text ?? "";

            var regex = new Regex(@"^-?\d+([.,]\d+)?$");

            if (string.IsNullOrWhiteSpace(input) || regex.IsMatch(input))
            {
                entry.TextColor = Colors.Black;
            }
            else
            {
                entry.TextColor = Colors.Red;
            }
        }
        private void DisplayWeather(WeatherData weather)
        {
            resultList.ItemsSource = new List<string>
            {
                $"City: {weather.City.Name}",
                $"Coordinates: Lat={weather.City.Lat}, Lon={weather.City.Lon}",
                $"Timezone: {weather.City.Timezone}",
                $"Temperature: {weather.Temperature}°C",
                $"Humidity: {weather.Humidity}%",
                $"Pressure: {weather.Pressure} hPa",
                $"Weather: {weather.WeatherDescription}",
                $"Wind Speed: {weather.WindSpeed} m/s",
                $"Sunrise: {DateTimeOffset.FromUnixTimeSeconds(weather.Sunrise).ToLocalTime()}",
                $"Sunset: {DateTimeOffset.FromUnixTimeSeconds(weather.Sunset).ToLocalTime()}"
            };
        }
        private async Task ClearDatabaseAsync()
        {
            try
            {
                db.Cities.RemoveRange(db.Cities);
                db.WeatherRecords.RemoveRange(db.WeatherRecords);

                await db.SaveChangesAsync();
                LoadCityList();
                await DisplayAlert("Sukces", "Baza danych została wyczyszczona.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", $"Błąd podczas czyszczenia bazy danych: {ex.Message}", "OK");
            }
        }
        private async void OnClearDatabaseClicked(object sender, EventArgs e)
        {
            await ClearDatabaseAsync();
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

}
