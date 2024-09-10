using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
