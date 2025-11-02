using System.Text.Json;
using ShopTARge24.Models.OpenWeather;


namespace ShopTARge24.ApplicationServices.Services
{
    public class OpenWeatherServices
    {
        private readonly HttpClient _httpClient;

        public OpenWeatherServices()
        {
            _httpClient = new HttpClient();
        }

        public async Task<OpenWeatherViewModel> GetCityWeather(string cityName)
        {
            const string ApiKey = "";
            const string Url = "https://api.openweathermap.org/data/2.5/weather";

            if (string.IsNullOrEmpty(cityName))
                return null;

            var url = $"{Url}?q={cityName}&appid={ApiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            return new OpenWeatherViewModel
            {
                CityName = root.GetProperty("name").GetString() ?? "",
                TempValue = root.GetProperty("main").GetProperty("temp").GetDouble(),
                TempFeelsLike = root.GetProperty("main").GetProperty("feels_like").GetDouble(),
                Humidity = root.GetProperty("main").GetProperty("humidity").GetInt32(),
                Pressure = root.GetProperty("main").GetProperty("pressure").GetInt32(),
                WindSpeed = root.GetProperty("wind").GetProperty("speed").GetDouble(),
                WeatherCondition = root.GetProperty("weather")[0].GetProperty("main").GetString()
            };
        }
    }
}