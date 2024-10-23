using Contract_Monthly_Claim.Data;
using Contract_Monthly_Claim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Contract_Monthly_Claim.Controllers
{
    public class LecturerController : Controller
    {
        //Working Code
        [HttpPost]
        public IActionResult Register(LecturerModel lecturer)
        {
            if (lecturer.FirstName.Length < 4)
            {
                TempData["ErrorMessage"] = "Username must be at least 4 characters long.";
                return RedirectToAction("Register", "Home");
            }

            if (!IsValidPassword(lecturer.Password))
            {
                TempData["ErrorMessage"] = "Password must contain at least one uppercase letter, one number, and be at least 4 characters long.";
                return RedirectToAction("Register", "Home");
            }

            if (!IsValidEmail(lecturer.Email))
            {
                TempData["ErrorMessage"] = "Email format is not valid.";
                return RedirectToAction("Register", "Home");
            }


            if (Database.GetLecturerByEmail(lecturer.Email) == null)
            {
                Database.AddLecturer(lecturer);

                TempData["SuccessMessage"] = "Account Created Successfully.";

                return RedirectToAction("Register", "Home"); ;
            }
            
            TempData["ErrorMessage"] = "Lecturer already exists.";
            return RedirectToAction("Index", "Home");
        }        

        //Working Code
        [HttpPost]
        public IActionResult Login(string email, string password)
        {            
            if(Database.ValidateLecturer(email, password))
            {
                LecturerModel lecturer = Database.GetLecturerByEmail(email);
                HttpContext.Session.SetString("UID", lecturer.LecturerId.ToString());
                HttpContext.Session.SetString("IsAuthorized", "true");
                
                HttpContext.Session.SetString("admin", "false");

                return RedirectToAction("Index", "Dashboard");
            }
            TempData["ErrorMessage"] = "Invalid email or password.";
            return RedirectToAction("Index", "Home");
        }

        //Working Code
        [HttpGet]
        public IActionResult GetLecturer(int lecturerId)
        {
            LecturerModel lecturer = Database.GetLecturer(lecturerId);

            if (lecturer != null)
            {
                return Ok(lecturer);
            }

            return NotFound("Lecturer not found.");
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

