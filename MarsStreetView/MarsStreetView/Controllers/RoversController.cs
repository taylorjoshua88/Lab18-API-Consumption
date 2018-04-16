using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MarsStreetView.Data;
using MarsStreetView.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace MarsStreetView.Controllers
{
    public class RoversController : Controller
    {
        private readonly MarsStreetViewDbContext _context;

        public static Uri RoverBaseURI {
            get => new Uri("https://api.nasa.gov/mars-photos/api/v1/rovers/");
        }

        // TODO: Move this to appsettings.json
        public static string NasaAPIKey {
            get => "sxi3tMyvfLuZWBlmbrzZ9RHBgKqBj8KWNfisUyxc";
        }

        public RoversController(MarsStreetViewDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? earthDate,
            string camera, string rover)
        {
            if (string.IsNullOrWhiteSpace(rover))
            {
                rover = "curiosity";
            }
            if (!earthDate.HasValue)
            {
                // Spirit stopped sending imagery on March 22nd, 2010
                earthDate = (rover == "spirit") ? DateTime.Parse("2010-3-21") :
                    DateTime.Today - TimeSpan.FromDays(1.0);
            }

            StringBuilder requestSB = new StringBuilder($"{rover.ToLower()}/photos?");

            requestSB.AppendJoin('&',
                $"earth_date={earthDate.Value.ToString("yyyy-MM-dd")}",
                $"api_key={NasaAPIKey}");

            if (!string.IsNullOrWhiteSpace(camera))
            {
                requestSB.Append($"&camera={camera.ToLower()}");
            }

            // Send our API request
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = RoverBaseURI;
                HttpResponseMessage response = await client.GetAsync(requestSB.ToString());

                if (response.IsSuccessStatusCode)
                {
                    RoverIndexViewModel roverVM = new RoverIndexViewModel()
                    {
                        Camera = camera,
                        EarthDate = earthDate.Value,
                        Rover = rover,
                        ResponseJSON = JObject.Parse(await response.Content.ReadAsStringAsync())
                    };

                    if (roverVM.ResponseJSON.HasValues && roverVM.ResponseJSON["photos"].Count() > 0)
                    {
                        roverVM.CameraList = new SelectList(roverVM.ResponseJSON["photos"].FirstOrDefault()["rover"]["cameras"].Select(c => c["name"]));
                    }

                    return View(roverVM);
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}