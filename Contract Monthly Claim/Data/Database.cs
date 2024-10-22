using Contract_Monthly_Claim.Models;
using Newtonsoft.Json;

namespace Contract_Monthly_Claim.Data
{
    public class Database
    {
        private static readonly string lectures = Path.Combine(Directory.GetCurrentDirectory(), "Data", "lecturers.json"); 
        private static readonly string managersJson = Path.Combine(Directory.GetCurrentDirectory(), "Data", "managers.json"); 
        
        //Load all lecturers from the JSON file
        private static List<LecturerModel> LoadLecturers()
        {
            if (File.Exists(lectures))
            {
                var jsonData = File.ReadAllText(lectures);
                return JsonConvert.DeserializeObject<List<LecturerModel>>(jsonData) ?? new List<LecturerModel>();
            }
            return new List<LecturerModel>();
        }

        //Write lecturer to the JSON file
        public static void SaveLecturers(List<LecturerModel> lecturers)
        {
            var jsonData = JsonConvert.SerializeObject(lecturers, Formatting.Indented);
            File.WriteAllText(lectures, jsonData);
        }

        //Get all lecturers and save them
        public static void AddLecturer(LecturerModel lecturer)
        {
            var lecturers = LoadLecturers();
            lecturer.LecturerId = lecturers.Count + 1;
            lecturers.Add(lecturer);
            SaveLecturers(lecturers);
        }

        // Get a lecturer by their ID
        public static LecturerModel GetLecturer(int lecturerId)
        {
            // Load all lecturers from the JSON file
            var lecturers = LoadLecturers();

            // Find the lecturer with the matching ID
            var foundLecturer = lecturers.FirstOrDefault(l => l.LecturerId == lecturerId);

            return foundLecturer;
        }

        // Get a lecturer by email 
        public static LecturerModel GetLecturerByEmail(string email)
        {       
            var json = File.ReadAllText(lectures);

            var lecturers = JsonConvert.DeserializeObject<List<LecturerModel>>(json);

            var foundLecturer = lecturers.FirstOrDefault(lecturer => lecturer.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) ;
            
            return foundLecturer;
        }

        // Validate lecturer credentials by email and password
        public static bool ValidateLecturer(string email, string password)
        {

            var lecturer = GetLecturerByEmail(email);
            
            return lecturer != null && lecturer.Password == password;
        }

        // Load all managers from the JSON file
        private static List<AcademicManagerModel> LoadManagers()
        {
            if (File.Exists(managersJson))
            {
                var jsonData = File.ReadAllText(managersJson);
                return JsonConvert.DeserializeObject<List<AcademicManagerModel>>(jsonData) ?? new List<AcademicManagerModel>();
            }
            return new List<AcademicManagerModel>();
        }

        // Save manager to the JSON file
        public static void SaveManagers(List<AcademicManagerModel> managers)
        {
            var jsonData = JsonConvert.SerializeObject(managers, Formatting.Indented);
            File.WriteAllText(managersJson, jsonData);
        }

        // Add a manager to the JSON file
        public static void AddAcademicManager(AcademicManagerModel manager)
        {
            var managers = LoadManagers();
            manager.ManagerId = managers.Count + 1;
            managers.Add(manager);
            SaveManagers(managers);
        }

        // Get all managers
        public static List<AcademicManagerModel> GetAllManagers()
        {
            return LoadManagers(); 
        }

        // Get a manager by ID
        public static AcademicManagerModel GetAcademicManager(int managerId)
        {
            var managers = LoadManagers();

            var foundManager = managers.FirstOrDefault(manager => manager.ManagerId == managerId);

            return foundManager;
        }

        // Get a manager by email
        public static AcademicManagerModel GetAcademicManagerByEmail(string email)
        {
            var json = File.ReadAllText(lectures);

            var managers = JsonConvert.DeserializeObject<List<AcademicManagerModel>>(json);

             var foundManager = managers.FirstOrDefault(manager => manager.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            return foundManager;
        }

        // Validate manager credentials by email and password
        public static bool ValidateAcademicManager(string email, string password)
        {   
            var manager = GetAcademicManagerByEmail(email);
            return manager != null && manager.Password == password;
        }
    }
}
