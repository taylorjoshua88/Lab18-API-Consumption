using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsStreetView.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public string Href { get; set; }
        public int Sol { get; set; }
        public string EarthDate { get; set; }
        public string CameraName { get; set; }
        public string RoverName { get; set; }

        public FavoriteList List { get; set; }
    }
}
