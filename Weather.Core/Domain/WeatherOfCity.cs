using Newtonsoft.Json;

namespace Weather.Core.Domain
{
    public class WeatherOfCity
    {
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("currentConditions")]
        public CurrentConditions CurrentConditions { get; set; }
    }

    public class CurrentConditions
    {
        [JsonProperty("dayhour")]
        public string DayHour { get; set; }
        
        [JsonProperty("temp")]
        public Temp Temperature { get; set; }
        
        [JsonProperty("precip")]
        public string Precipitation { get; set; }
        
        [JsonProperty("humidity")]
        public string Humidity { get; set; }
        
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        
        [JsonProperty("iconURL")]
        public string IconUrl { get; set; }
        
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }

    public class Temp
    {
        [JsonProperty("c")]
        public string C { get; set; }
        
        [JsonProperty("f")]
        public string F { get; set; }
    }

    public class Wind
    {
        
        [JsonProperty("km")]
        public string Km { get; set; }
        
        [JsonProperty("mile")]
        public string Mile { get; set; }
    }

    public class WeatherView
    {
        public string City { get; set; }
        public string CelciusTemp { get; set; }
        public string FahrenheitTemp { get; set; }
        public string Precipitation { get; set; }
        public string Humidity { get; set; }
        public string Km { get; set; }
        public string Mile { get; set; }
        public string Comment { get; set; }

        public static WeatherView ConvertFrom(WeatherOfCity weatherOfCity)
        {
            if (weatherOfCity == null)
            {
                return null;
            }
            
            return new WeatherView()
            {
                City = weatherOfCity.Region,
                CelciusTemp = weatherOfCity?.CurrentConditions?.Temperature.C,
                FahrenheitTemp = weatherOfCity?.CurrentConditions?.Temperature.F,
                Comment = weatherOfCity?.CurrentConditions?.Comment,
                Precipitation = weatherOfCity?.CurrentConditions?.Precipitation,
                Humidity = weatherOfCity?.CurrentConditions?.Humidity,
                Km = weatherOfCity?.CurrentConditions?.Wind?.Km,
                Mile = weatherOfCity?.CurrentConditions?.Wind?.Mile
            };
        }
    }
}