using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class MapGeometryDto
    {
        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string Name;
        public string Address { get; set; }
        public string UrlSlug { get; set; }
    }
}
