#if false

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsStreetView.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int Sol { get; set; }

        JObject Camera;

        [JsonProperty(PropertyName = "img_src")]
        public string ImgSrc { get; set; }

        [JsonProperty(PropertyName = "earth_date")]
        public string EarthDate { get; set; }

        public JObject Rover { get; set; }
    }
}

#endif