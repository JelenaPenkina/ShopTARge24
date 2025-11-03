namespace ShopTARge24.Core.Dto
{
    public class OpenWeatherRootDto
    {
        public string CityName { get; set; } = string.Empty;
        public double? TempValue { get; set; }
        public double? TempFeelsLike { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string? WeatherCondition { get; set; }
    }
}
