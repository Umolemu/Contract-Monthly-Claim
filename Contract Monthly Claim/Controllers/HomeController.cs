using Contract_Monthly_Claim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Contract_Monthly_Claim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Logout action
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(TempData["ErrorMessage"]?.ToString()))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
            }

            if (!string.IsNullOrEmpty(TempData["SuccessMessage"]?.ToString()))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"]?.ToString();
            }

            return View();
        }

        public IActionResult Register()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
