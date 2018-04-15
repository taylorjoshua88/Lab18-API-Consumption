using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MarsStreetView.Data;
using Microsoft.AspNetCore.Mvc;
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
            string rover = "curiosity", string camera = "fhaz")
        {
            StringBuilder requestSB = new StringBuilder($"{rover}/photos?");

            requestSB.AppendJoin('&',
                $"earth_date={(earthDate ?? DateTime.Today - TimeSpan.FromDays(1.0)).ToString("yyyy-MM-dd")}",
                $"camera={camera}",
                $"api_key={NasaAPIKey}");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = RoverBaseURI;
                HttpResponseMessage response = await client.GetAsync(requestSB.ToString());

                if (response.IsSuccessStatusCode)
                {
                    return View(JObject.Parse(await response.Content.ReadAsStringAsync()));
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}