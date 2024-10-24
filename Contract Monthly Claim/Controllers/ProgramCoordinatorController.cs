using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Contract_Monthly_Claim.Models;
using Contract_Monthly_Claim.Data;
using System.Text.RegularExpressions;

namespace Contract_Monthly_Claim.Controllers
{
    [Route("ProgramCoordinator")]
    public class ProgramCoordinatorController : Controller
    {
        private readonly string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Coordinators.json");

        // Register a new Program Coordinator
        [HttpPost("Register")]
        public IActionResult Register(ProgrammeCoordinatorModel coordinator)
        {
            if (coordinator.FirstName.Length < 4)
            {
                TempData["ErrorMessage"] = "Name must be at least 4 characters long.";
                return RedirectToAction("Register", "Coordinator");
            }

            if (coordinator.LastName.Length < 4)
            {
                TempData["ErrorMessage"] = "Last name must be at least 4 characters long.";
                return RedirectToAction("Register", "Coordinator");
            }

            if (!IsValidPassword(coordinator.Password))
            {
                TempData["ErrorMessage"] = "Password must contain at least one uppercase letter, one number, and be at least 4 characters long.";
                return RedirectToAction("Register", "Coordinator");
            }

            if (!IsValidEmail(coordinator.Email))
            {
                TempData["ErrorMessage"] = "Email format is not valid.";
                return RedirectToAction("Register", "Coordinator");
            }

            if (Database.GetProgrammeCoordinatorByEmail(coordinator.Email) == null)
            {
                Database.AddProgrammeCoordinator(coordinator);

                TempData["SuccessMessage"] = "Account Created Successfully.";
                return RedirectToAction("Register", "Coordinator");
            }

            TempData["ErrorMessage"] = "Coordinator already exists.";
            return RedirectToAction("Index", "Coordinator");
        }

        // Login functionality for Program Coordinator
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            if (Database.ValidateProgrammeCoordinator(email, password))
            {
                ProgrammeCoordinatorModel coordinator = Database.GetProgrammeCoordinatorByEmail(email);
                HttpContext.Session.SetString("UID", coordinator.CoordinatorId.ToString());
                HttpContext.Session.SetString("IsAuthorized", "true");
                HttpContext.Session.SetString("admin", "true");

                HttpContext.Session.SetString("admin", "true");

                HttpContext.Session.SetString("coordinator", "true");
                HttpContext.Session.SetString("manager", "false");


                return RedirectToAction("Index", "Dashboard");
            }

            return RedirectToAction("Index", "Coordinator");
        }

        // View details of a specific Program Coordinator
        [HttpGet("View")]
        public IActionResult GetCoordinator(int coordinatorId)
        {
            ProgrammeCoordinatorModel coordinator = Database.GetProgrammeCoordinator(coordinatorId);

            if (coordinator != null)
            {
                return Ok(coordinator);
            }

            return NotFound("Coordinator not found.");
        }

        // Validate password
        private bool IsValidPassword(string password)
        {
            return password.Length >= 4 &&
                   Regex.IsMatch(password, @"[A-Z]") &&
                   Regex.IsMatch(password, @"[0-9]");
        }

        // Validate the email format
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
