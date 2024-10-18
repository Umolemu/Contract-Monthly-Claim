using Contract_Monthly_Claim.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Claims;

namespace Contract_Monthly_Claim.Data
{
    public class Database
    {
        private static readonly string lectures = Path.Combine(Directory.GetCurrentDirectory(), "Data", "lecturers.json"); 
        private static readonly string managersJson = Path.Combine(Directory.GetCurrentDirectory(), "Data", "managers.json"); 
        
        private static List<LecturerModel> LoadLecturers()
        {
            if (File.Exists(lectures))
            {
                var jsonData = File.ReadAllText(lectures);
                return JsonConvert.DeserializeObject<List<LecturerModel>>(jsonData) ?? new List<LecturerModel>();
            }
            return new List<LecturerModel>();
        }

        // Method to write lecturers to the JSON file
        public static void SaveLecturers(List<LecturerModel> lecturers)
        {
            var jsonData = JsonConvert.SerializeObject(lecturers, Formatting.Indented);
            File.WriteAllText(lectures, jsonData);
        }

        public static void AddLecturer(LecturerModel lecturer)
        {
            var lecturers = LoadLecturers();
            lecturer.LecturerId = lecturers.Count + 1;
            lecturers.Add(lecturer);
            SaveLecturers(lecturers);
        }

        // Get all lecturers from the JSON file
        public static List<LecturerModel> GetAllLecturers()
        {
            return LoadLecturers();
        }

        // Get a lecturer by their ID
        public static LecturerModel GetLecturer(int lecturerId)
        {
            return LoadLecturers().FirstOrDefault(l => l.LecturerId == lecturerId);
        }

        // Get a lecturer by email
        public static LecturerModel GetLecturerByEmail(string email)
        {
            email = "Test1@example.com";
            Console.WriteLine($"Lecturer file path: {lectures}");

            // Read the JSON data from the file
            var json = File.ReadAllText(lectures);

            // Deserialize the JSON into a list of LecturerModel
            var lecturers = JsonConvert.DeserializeObject<List<LecturerModel>>(json);
                        
            var foundLecturer = lecturers.FirstOrDefault(l => l.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            return foundLecturer;
        }

        // Validate lecturer credentials by email and password
        public static bool ValidateLecturer(string email, string password)
        {
            var lecturer = GetLecturerByEmail(email);
            
            Console.WriteLine(lecturer);

            return lecturer != null && lecturer.Password == password;
        }

        private static List<AcademicManagerModel> LoadManagers()
        {
            if (File.Exists(managersJson))
            {
                var jsonData = File.ReadAllText(managersJson);
                return JsonConvert.DeserializeObject<List<AcademicManagerModel>>(jsonData) ?? new List<AcademicManagerModel>();
            }
            return new List<AcademicManagerModel>();
        }

        public static void SaveManagers(List<AcademicManagerModel> managers)
        {
            var jsonData = JsonConvert.SerializeObject(managers, Formatting.Indented);
            File.WriteAllText(managersJson, jsonData);
        }

        public static void AddAcademicManager(AcademicManagerModel manager)
        {
            var managers = LoadManagers();
            manager.ManagerId = managers.Count + 1;
            managers.Add(manager);
            SaveManagers(managers);
        }

        public static List<AcademicManagerModel> GetAllManagers()
        {
            return LoadManagers(); 
        }

        public static AcademicManagerModel GetAcademicManager(int managerId)
        {
            return LoadManagers().FirstOrDefault(m => m.ManagerId == managerId);
        }

        // Get a manager by email
        public static AcademicManagerModel GetAcademicManagerByEmail(string email)
        {
            Console.WriteLine($"Managers file path: {managersJson}");

            var json = System.IO.File.ReadAllText(managersJson);

            var managers = JsonConvert.DeserializeObject<List<Contract_Monthly_Claim.Models.AcademicManagerModel>>(json);

            // Find and return the manager with the matching email (case insensitive)
            var foundManager = managers.FirstOrDefault(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            // Print the return value (found manager)
            if (foundManager != null)
            {
                Console.WriteLine($"Found Manager: {foundManager.FirstName}, Email: {foundManager.Email}");
            }
            else
            {
                Console.WriteLine("Manager not found.");
            }

            return foundManager;
        }

        public static bool ValidateAcademicManager(string email, string password)
        {
            var manager = GetAcademicManagerByEmail(email);
            return manager != null && manager.Password == password;
        }
    }
}
