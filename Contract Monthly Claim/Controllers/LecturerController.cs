using Contract_Monthly_Claim.Data;
using Contract_Monthly_Claim.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim.Controllers
{
    public class LecturerController : Controller
    {
        [HttpPost]
        public IActionResult Register(LecturerModel lecturer)
        {
            if (Database.GetLecturerByEmail(lecturer.Email) == null)
            {
                Database.AddLecturer(lecturer);

                return RedirectToAction("Index", "Home");
            }
            return BadRequest("Lecturer already exists.");
        }        

        [HttpPost]
        public IActionResult Login(string email, string password)
        {            
            LecturerModel lecturer = Database.GetLecturerByEmail(email);
            HttpContext.Session.SetString("UID", lecturer.LecturerId.ToString());
            HttpContext.Session.SetString("IsAuthorized", "true");
            HttpContext.Session.SetString("IsAdmin", "false");

            return RedirectToAction("Index", "Dashboard");
        }

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
    }
}

