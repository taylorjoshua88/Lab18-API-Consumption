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
            return View(await _context.FavoriteList.OrderBy(l => l.Name).ToListAsync());
        }

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
    }
}