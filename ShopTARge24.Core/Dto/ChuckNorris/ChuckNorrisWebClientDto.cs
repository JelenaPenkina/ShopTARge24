using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopTARge24.Core.Dto.ChuckNorris
{
    public class ChuckNorrisWebClientDto
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Categories")]
        public string[] Categories { get; set; }

        [JsonPropertyName("iconUrl")]
        public string iconUrl { get; set; }

        [JsonPropertyName("Url")]
        public string Url { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }

        [JsonPropertyName("createdAt")]
        public string createdAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public string updatedAt { get; set; }
    }
}
