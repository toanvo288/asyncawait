using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Weather.Core.Domain;

namespace Weather.Core.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherView>> GetWeatherOfCityAsync(string cityName,
            CancellationToken cancellationToken);
        
        IEnumerable<WeatherView> GetWeatherOfCity(string cityName);

        void DownloadWeatherCity(string cityName);
    }
    
    public class WeatherService : IWeatherService
    {
        private static string API_URL = "https://weatherdbi.herokuapp.com/data/weather";
        
        public async Task<IEnumerable<WeatherView>> GetWeatherOfCityAsync(string cityName, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{API_URL}/{cityName}",
                    cancellationToken);
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                var weatherOfCity = JsonConvert.DeserializeObject<WeatherOfCity>(content);
                return new List<WeatherView>() { WeatherView.ConvertFrom(weatherOfCity) };
            }
        }

        public IEnumerable<WeatherView> GetWeatherOfCity(string cityName)
        {
            using (var client = new WebClient())
            {
                var taskContent = client.DownloadString($"{API_URL}/{cityName}");
                var weatherOfCity = JsonConvert.DeserializeObject<WeatherOfCity>(taskContent);
                yield return WeatherView.ConvertFrom(weatherOfCity);
            }
        }

        public async void DownloadWeatherCity(string cityName)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{API_URL}/{cityName}");
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                // Do something in background service
                Debugger.Log(1, "WeatherApp", content);
            }
        }
    }
}