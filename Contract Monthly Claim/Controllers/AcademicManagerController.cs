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

        // Working Code
        [HttpPost("Register")]
        public IActionResult Register(AcademicManagerModel manager)
        {
            if (Database.GetAcademicManagerByEmail(manager.Email) == null)
            {
                Database.AddAcademicManager(manager);

                return RedirectToAction("Index", "Manager");
            }

            return BadRequest("Manager already exists.");
        }

        // Working Code
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            if(Database.ValidateAcademicManager(email, password))
            {
                AcademicManagerModel manager = Database.GetAcademicManagerByEmail(email);
                HttpContext.Session.SetString("UID", manager.ManagerId.ToString());
                HttpContext.Session.SetString("IsAuthorized", "true");
               
                HttpContext.Session.SetString("admin", "true");

                return RedirectToAction("Index", "Dashboard");
            }
            return RedirectToAction("Index", "Manager");
        }

        [HttpGet("View")]
        public IActionResult GetManager(int managerId)
        {
            AcademicManagerModel manager = Database.GetAcademicManager(managerId);

            if (manager != null)
            {
                return Ok(manager);
            }

            return NotFound("Manager not found.");
        }
    }
}
