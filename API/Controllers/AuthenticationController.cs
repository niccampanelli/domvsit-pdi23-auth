﻿using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View("Olá mundo!");
        }
    }
}
