using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopTARge24.Core.Dto.AccuLocationRootDto;

namespace ShopTARge24.Core.Dto
{
    //public class AccuCityCodeRootDto
    //{
    //    public CityCode[]? Property { get; set; }

    //}

    public class AccuCityCodeRootDto
    {
        public string? PrimaryCode { get; set; }
        public int Version { get; set; }
        public string? Key { get; set; }
        public string? Type { get; set; }
        public int Rank { get; set; }
        public string? LocalizedName { get; set; }
        public string? EnglishName { get; set; }
        public AccuRegionDto? Region { get; set; }
        public AccuCountryDto? Country { get; set; }
        public AccuAdministrativeAreaDto? AdministrativeArea { get; set; }
        public AccuTimeZoneDto? TimeZone { get; set; }
        public AccuGeoPositionDto? GeoPosition { get; set; }
        public bool isAlias { get; set; }
        public SupplementalAdminArea[]? SupplementalAdminAreas { get; set; }
        public string[]? DataSets { get; set; }

        public class AccuRegionDto
        {
            public CountryAndRegionDto? Region { get; set; }
        }

        public class AccuCountryDto
        {
            public CountryAndRegionDto? Country { get; set; }
        }

        public class CountryAndRegionDto
        {
            public int Id { get; set; }
            public string? LocalizedName { get; set; }
            public string? EnglishName { get; set; }
        }
        public class AccuAdministrativeAreaDto
        {
            public string? Id { get; set; }
            public string? LocalizedName { get; set; }
            public string? EnglishName { get; set; }
            public int Level { get; set; }
            public string? LocalizedType { get; set; }
            public string? EnglishType { get; set; }
            public int CountryId { get; set; }
        }
        public class AccuTimeZoneDto
        {
            public string? Code { get; set; }
            public string? Name { get; set; }
            public int GmtOffset { get; set; }
            public bool IsDaylightSaving { get; set; }
            public DateTime NextOffsetChange { get; set; }
        }

        public class AccuGeoPositionDto
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public AccuElevation? Elevation { get; set; }

        }
        public class AccuElevation
        {
            public ElevationWeatherUnitDto? Metric { get; set; }
            public ElevationWeatherUnitDto? Imperail { get; set; }
        }

        public class ElevationWeatherUnitDto
        {
            public double Value { get; set; }
            public string? Unit { get; set; }
            public int UnitType { get; set; }
        }

        public class SupplementalAdminArea 
        {
            public int Level { get; set; }
            public string? LocalizedName { get; set; }
            public string? EnglishName { get; set; }
        }
    }    
}


