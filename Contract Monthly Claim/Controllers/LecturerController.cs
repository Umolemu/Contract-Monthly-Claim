using Contract_Monthly_Claim.Data;
using Contract_Monthly_Claim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim.Controllers
{
    public class LecturerController : Controller
    {
        //Working Code
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
    }
}

