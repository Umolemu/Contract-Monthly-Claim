using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Contract_Monthly_Claim.Models;
using Contract_Monthly_Claim.Data;
using System.Text.RegularExpressions;

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
            if (manager.FirstName.Length < 4)
            {
                TempData["ErrorMessage"] = "Name must be at least 4 characters long.";
                return RedirectToAction("Register", "Manager");
            }

            if (manager.LastName.Length < 4)
            {
                TempData["ErrorMessage"] = "Last name must be at least 4 characters long.";
                return RedirectToAction("Register", "Manager");
            }

            if (!IsValidPassword(manager.Password))
            {
                TempData["ErrorMessage"] = "Password must contain at least one uppercase letter, one number, and be at least 4 characters long.";
                return RedirectToAction("Register", "Manager");
            }

            if (!IsValidEmail(manager.Email))
            {
                TempData["ErrorMessage"] = "Email format is not valid.";
                return RedirectToAction("Register", "Manager");
            }

            if (Database.GetAcademicManagerByEmail(manager.Email) == null)
            {
                Database.AddAcademicManager(manager);

                TempData["SuccessMessage"] = "Account Created Successfully.";

                return RedirectToAction("Register", "Manager");
            }

            TempData["ErrorMessage"] = "Manager already exists.";
            return RedirectToAction("Index", "Manager");
        }

        // Working Code
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            if (Database.ValidateAcademicManager(email, password))
            {
                AcademicManagerModel manager = Database.GetAcademicManagerByEmail(email);
                HttpContext.Session.SetString("UID", manager.ManagerId.ToString());
                HttpContext.Session.SetString("IsAuthorized", "true");

                HttpContext.Session.SetString("admin", "true");

                HttpContext.Session.SetString("coordinator", "false");
                HttpContext.Session.SetString("manager", "true");

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

        // Password validation 
        private bool IsValidPassword(string password)
        {
            return password.Length >= 4 &&
                   Regex.IsMatch(password, @"[A-Z]") &&
                   Regex.IsMatch(password, @"[0-9]");
        }

        // Email validation
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
