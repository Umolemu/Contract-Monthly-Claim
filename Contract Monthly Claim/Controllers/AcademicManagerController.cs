using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Contract_Monthly_Claim.Models;
using Contract_Monthly_Claim.Data;

namespace Contract_Monthly_Claim.Controllers
{
    [Route("AcademicManager")]
    public class AcademicManagerController : Controller
    {
        private readonly string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Managers.json");

        [HttpPost("Register")]
        public IActionResult Register(AcademicManagerModel manager)
        {
            if (ModelState.IsValid)
            {
                var managers = LoadManagers();

                manager.ManagerId = managers.Count > 0 ? managers[^1].ManagerId + 1 : 1;

                managers.Add(manager);

                SaveManagers(managers);

                return RedirectToAction("Index", "Manager");
            }

            return View(manager);
        }

        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            HttpContext.Session.SetString("IsAuthorized", "true");
            HttpContext.Session.SetString("IsAdmin", "true");

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet("View")]
        public IActionResult ViewManagers()
        {
            var managers = LoadManagers();
            return View(managers); 
        }

        private List<AcademicManagerModel> LoadManagers()
        {
            if (!System.IO.File.Exists(jsonFilePath))
            {
                return new List<AcademicManagerModel>();
            }

            var json = System.IO.File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<List<AcademicManagerModel>>(json) ?? new List<AcademicManagerModel>();
        }

        private void SaveManagers(List<AcademicManagerModel> managers)
        {
            var json = JsonSerializer.Serialize(managers, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(jsonFilePath, json);
        }
    }
}
