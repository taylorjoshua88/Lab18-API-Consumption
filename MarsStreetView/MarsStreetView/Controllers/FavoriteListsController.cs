﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarsStreetView.Controllers
{
    public class FavoriteListsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}