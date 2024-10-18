using Contract_Monthly_Claim.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Contract_Monthly_Claim.Data
{
    public class Database
    {
        private static readonly string jsonFilePath = "Data/Lecturers.json";


        private static List<LecturerModel> LoadLecturers()
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonData = File.ReadAllText(jsonFilePath);
                return JsonConvert.DeserializeObject<List<LecturerModel>>(jsonData) ?? new List<LecturerModel>();
            }
            return new List<LecturerModel>();
        }

        // Method to write lecturers to the JSON file
        public static void SaveLecturers(List<LecturerModel> lecturers)
        {
            var jsonData = JsonConvert.SerializeObject(lecturers, Formatting.Indented);
            File.WriteAllText(jsonFilePath, jsonData);
        }

        // Add a lecturer directly to the JSON file
        public static void AddLecturer(LecturerModel lecturer)
        {
            var lecturers = LoadLecturers();
            lecturer.LecturerId = lecturers.Count + 1; // Assign unique ID
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
            return LoadLecturers().FirstOrDefault(l => l.Email == email);
        }

        // Validate lecturer credentials by email and password
        public static bool ValidateLecturer(string email, string password)
        {
            var lecturer = GetLecturerByEmail(email);
            Console.WriteLine("Lecturer found: " + lecturer?.Email);
            return lecturer != null && lecturer.Password == password;
        }
    }
}
