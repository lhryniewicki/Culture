using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class GeometryDto
    {
        [JsonProperty(PropertyName = "lat")]
        public decimal? Latitute { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public decimal? Longtitute { get; set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName;
    }
}