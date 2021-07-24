using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;



namespace Cooper_Lab12
{
    class Program
    {
        static void Main(string[] args)
        {   
            Console.WriteLine("enter city name: ");
            string city = Console.ReadLine();
            string api_key = getApiKey();
            var currentweather = _download_serialized_json_data<CurrentWeather>("http://api.openweathermap.org/data/2.5/weather?+q={city_name}&units=imperial&appid={api_key}");
            
            Console.WriteLine($"the tempareture of {city} is {currentweather}"); 
        }
        
        public static string getApiKey()
        {
             var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<Startup>();

            IConfigurationRoot configuration = builder.Build();
            return configuration["key"];
        }


        private static T _download_serialized_json_data<T>(string url) where T : new()
        {

            using (var w = new WebClient())
            {

                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString("http://api.openweathermap.org/data/2.5/weather?+q={city_name}&units=imperial&appid={key}");
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
        public class Startup
        {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        }

    }
}
    public class CurrentWeather 
    {
        public float temp { get; set; }
    }