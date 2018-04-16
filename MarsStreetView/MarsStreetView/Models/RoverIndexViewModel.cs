using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarsStreetView.Models
{
    public class RoverIndexViewModel
    {
        public string Rover { get; set; }

        public string Camera { get; set; }
        public SelectList CameraList { get; set; }

        [Display(Name = "Earth Date")]
        [DataType(DataType.Date)]
        public DateTime EarthDate { get; set; }

        public JObject ResponseJSON { get; set; }
    }
}
