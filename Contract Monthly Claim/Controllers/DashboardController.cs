﻿using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Track()
        {
            return View();
        }
    }
}
