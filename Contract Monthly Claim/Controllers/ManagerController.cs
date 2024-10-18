using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
