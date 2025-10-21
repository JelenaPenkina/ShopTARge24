namespace ShopTARge24.Core.Dto
{
    public class AccuLocationRootDto
    {

        public Headline Headline { get; set; }
        public Dailyforecast[] DailyForecasts { get; set; }
    }

    public class Headline
    {
        public string EffectiveDate { get; set; }
        public int EffectiveEpochDate { get; set; }
        public int Severity { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public string EndDate { get; set; }
        public int EndEpochDate { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class Dailyforecast
    {
        public string Date { get; set; }
        public int EpochDate { get; set; }
        public Temperature Temperature { get; set; }
        public Day Day { get; set; }
        public Night Night { get; set; }
        public string[] Sources { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class Temperature
    {
        public Minimum Minimum { get; set; }
        public Maximum Maximum { get; set; }
    }

    public class Minimum
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Maximum
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Day
    {
        public int Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public string PrecipitationIntensity { get; set; }
    }

    public class Night
    {
        public int Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }

    }
}

// Algne versioon 

//public string? LocalObservationDateTime { get; set; }
//public int EpochTime { get; set; }
//public string? WeatherText { get; set; }
//public int WeatherIcon { get; set; }
//public bool HasPrecipitation { get; set; }
//public string? PrecipitationType { get; set; }
//public bool IsDayTime { get; set; }

//public AccuTemperatureDto? Temperature { get; set; }

//public string? MobileLink { get; set; }
//public string? Link { get; set; }
//public class AccuTemperatureDto
//{
//    public AccuWeatherUnitDto? Metric { get; set; }
//    public AccuWeatherUnitDto? Imperail { get; set; }
//}

//public class AccuWeatherUnitDto 
//{
//    public double Value { get; set; }
//    public string? Unit { get; set; }
//    public int UnitType { get; set; }
//}

//public class AccuMericDto -> minu versioon 
//{
//    public double Value { get; set; }
//    public string Unit { get; set; }
//    public int UnitType { get; set; }

//}

//public class AccuImperailDto
//{
//    public double Value { get; set; }
//    public string Unit { get; set; }
//    public int UnitType { get; set; }
//}




