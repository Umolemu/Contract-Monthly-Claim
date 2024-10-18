using Contract_Monthly_Claim.Data;
using Contract_Monthly_Claim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim.Controllers
{
    public class LecturerController : Controller
    {
        // Registers a new lecturer
        [HttpPost]
        public IActionResult Register(LecturerModel lecturer)
        {
            // Check if the lecturer with the provided email already exists
            if (Database.GetLecturerByEmail(lecturer.Email) == null)
            {
                // Add lecturer to the JSON file
                Database.AddLecturer(lecturer);

                // Debug: Log the successful registration
                Console.WriteLine("Lecturer registered: " + lecturer.Email + " " + lecturer.Password);

                // Redirect to the login page after successful registration
                return RedirectToAction("Index", "Home");
            }
            return BadRequest("Lecturer already exists.");
        }

        // Lecturer login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Log the login attempt details
            Console.WriteLine("Login attempt: " + email + " " + password);

            // Validate the email and password
            //if (Database.ValidateLecturer(email, password))
            //{
                // Set session to indicate the lecturer is authorized
                HttpContext.Session.SetString("IsAuthorized", "true");
                HttpContext.Session.SetString("IsAdmin", "false");

                // Redirect to the dashboard after successful login
                return RedirectToAction("Index", "Dashboard");
            //}

        }

        // Retrieves a lecturer by their ID
        [HttpGet]
        public IActionResult GetLecturer(int lecturerId)
        {
            // Retrieve the lecturer from the JSON data
            LecturerModel lecturer = Database.GetLecturer(lecturerId);

            if (lecturer != null)
            {
                // Log the found lecturer's email
                Console.WriteLine("Lecturer found: " + lecturer.Email);
                return Ok(lecturer);
            }

            // Log if no lecturer was found
            Console.WriteLine("No lecturer found with ID: " + lecturerId);
            return NotFound("Lecturer not found.");
        }


        // Updates lecturer information
        [HttpPost]
        public IActionResult UpdateLecturer(int lecturerId, LecturerModel updatedLecturer)
        {
            var lecturer = Database.GetLecturer(lecturerId);
            if (lecturer != null)
            {
                // Update the lecturer's details
                lecturer.FirstName = updatedLecturer.FirstName;
                lecturer.Email = updatedLecturer.Email;
                lecturer.Password = updatedLecturer.Password;

                // Save changes to the JSON file
                Database.SaveLecturers(Database.GetAllLecturers()); // Assuming SaveLecturers method was added

                return Ok("Lecturer details updated.");
            }
            return NotFound("Lecturer not found.");
        }

        // Deletes a lecturer by ID
        [HttpDelete]
        public IActionResult DeleteLecturer(int lecturerId)
        {
            var lecturers = Database.GetAllLecturers();
            var lecturer = lecturers.FirstOrDefault(l => l.LecturerId == lecturerId);

            if (lecturer != null)
            {
                lecturers.Remove(lecturer);
                Database.SaveLecturers(lecturers); // Save changes to the JSON file
                return Ok("Lecturer deleted.");
            }
            return NotFound("Lecturer not found.");
        }
    }
}

