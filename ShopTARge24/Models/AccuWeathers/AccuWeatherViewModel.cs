namespace ShopTARge24.Models.AccuWeathers
{
    public class AccuWeatherViewModel
    {
        public string? CityName { get; set; }
        public string? EffectiveCode { get; set; }
        public int Severity { get; set; }
        public string? Category {  get; set; }
        public string? WeatherText { get; set; }
        public double? TempMaxCelsius { get; set; }
        public double? TempMinCelsius { get; set; }
    }
}
