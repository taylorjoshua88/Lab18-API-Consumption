using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarsStreetView.Data;
using MarsStreetView.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarsStreetView.Controllers
{
    public class FavoriteListsController : Controller
    {
        private readonly MarsStreetViewDbContext _context;

        public FavoriteListsController(MarsStreetViewDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (_context.FavoriteList.Count() < 1)
            {
                TempData["NotificationType"] = "alert-info";
                TempData["NotificationMessage"] = "There are no favorite lists yet. Please create one now.";
                return RedirectToAction("Create");
            }

            return View(await _context.FavoriteList.Include(l => l.Favorites)
                                                   .OrderBy(l => l.Name)
                                                   .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> List(int? id)
        {
            // If no ID has been provided just redirect to index
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            try
            {
                return View(await _context.FavoriteList.Include(l => l.Favorites)
                                                       .FirstAsync(l => l.Id == id.Value));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new FavoriteList());
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            {
                TempData["NotificationType"] = "alert-warning";
                TempData["NotificationMessage"] = "Favorite list names must be at least 3 characters long.";
                return View(new FavoriteList() { Name = name });
            }

            FavoriteList list = await _context.FavoriteList.FirstOrDefaultAsync(l => l.Name == name);

            if (list != null)
            {
                TempData["NotificationType"] = "alert-danger";
                TempData["NotificationMessage"] = "A favorite list of that name already exists! Please try again.";
                return View(new FavoriteList() { Name = name });
            }

            list = new FavoriteList() { Name = name };

            await _context.FavoriteList.AddAsync(list);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                // TODO: Add logging here
                TempData["NotificationType"] = "alert-danger";
                TempData["NotificationMessage"] = "Could not commit the new favorite list to the database. Please try again.";
                return View(new FavoriteList() { Name = name });
            }

            TempData["NotificationType"] = "alert-success";
            TempData["NotificationMessage"] = $"Successfully created the {list.Name} favorite list!";
            return RedirectToAction("List", list.Id);
        }

        [HttpGet]
        public async Task<IActionResult> AddFavorite(string rover, string cameraName,
            string cameraFullName, string earthDate, int sol, string href, int nasaId)
        {
            Favorite favorite = 
                await _context.Favorite.FirstOrDefaultAsync(f => f.NasaID == nasaId);

            AddFavoriteViewModel viewModel = new AddFavoriteViewModel()
            {
                Existing = favorite != null,
                Lists = await _context.FavoriteList.ToListAsync(),
                Favorite = favorite ?? new Favorite
                {
                    RoverName = rover,
                    CameraName = cameraName,
                    CameraFullName = cameraFullName,
                    EarthDate = DateTime.Parse(earthDate),
                    Sol = sol,
                    NasaID = nasaId,
                    Href = href,
                    List = null
                }
            };

            return View(viewModel);
        }
    }
}