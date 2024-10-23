using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Claims()
        {
            return View();
        }

        public IActionResult ApproveOrReject()
        {
            return View();
        }
    }
}
