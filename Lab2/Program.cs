using System;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab2
{
    public class APITest
    {
        public HttpClient client;
        public async Task GetData()
        {
            client = new HttpClient();
            string call = "https://api.openweathermap.org/data/2.5/weather?lat=51.11&lon=17.04&appid=6115f0cc1473a569cf89607b7072e0dd";
            string response = await client.GetStringAsync(call);

            ToDo data = JsonSerializer.Deserialize<ToDo>(response);
            Console.WriteLine(data);

            /*
            List<ToDo> data = JsonSerializer.Deserialize<List<ToDo>>(response);
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }*/
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            APITest t = new APITest();
            t.GetData().Wait();
        }
    }

    internal class ToDo
    {
        public Coord coord { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public int timezone { get; set; }

        public override string ToString()
        {
            return $"Coord: lat={coord.lat}, lon={coord.lon}, name={name}, id={id}, timezone={timezone}";
        }
    }
    internal class Coord
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
}