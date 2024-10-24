using System.Collections.Generic;
using System.IO;
using System.Linq;
using Contract_Monthly_Claim.Data;
using Contract_Monthly_Claim.Models;
using Newtonsoft.Json;
using Xunit;

namespace Contract_Monthly_Claim.Tests
{
    public class DatabaseTests
    {
        private readonly string _lecturersPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "lecturers.json");
        private readonly string _managersPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "managers.json");

        // Ensure that the test data does not affect real data
        public DatabaseTests()
        {
            if (File.Exists(_lecturersPath))
                File.Delete(_lecturersPath);
            if (File.Exists(_managersPath))
                File.Delete(_managersPath);
        }

        [Fact]
        public void LoadLecturers_ShouldReturnEmptyList_WhenFileDoesNotExist()
        {
            // Act
            var result = Database.LoadLecturers();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void SaveLecturers_ShouldSaveLecturersToFile()
        {
            // Arrange
            var lecturers = new List<LecturerModel>
            {
                new LecturerModel { Email = "lecturer1@example.com", Password = "pass1" },
                new LecturerModel { Email = "lecturer2@example.com", Password = "pass2" }
            };

            // Act
            Database.SaveLecturers(lecturers);

            // Assert
            var savedData = File.ReadAllText(_lecturersPath);
            var deserializedLecturers = JsonConvert.DeserializeObject<List<LecturerModel>>(savedData);
            Assert.Equal(2, deserializedLecturers.Count);
        }

        [Fact]
        public void AddLecturer_ShouldAddLecturerAndSaveToFile()
        {
            // Arrange
            var lecturer = new LecturerModel { Email = "lecturer1@example.com", Password = "pass1" };

            // Act
            Database.AddLecturer(lecturer);

            // Assert
            var savedData = File.ReadAllText(_lecturersPath);
            var deserializedLecturers = JsonConvert.DeserializeObject<List<LecturerModel>>(savedData);
            Assert.Single(deserializedLecturers);
            Assert.Equal("lecturer1@example.com", deserializedLecturers.First().Email);
        }

        [Fact]
        public void GetLecturer_ShouldReturnLecturer_WhenExists()
        {
            // Arrange
            var lecturer = new LecturerModel { Email = "lecturer1@example.com", Password = "pass1" };
            Database.AddLecturer(lecturer);

            // Act
            var result = Database.GetLecturer(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("lecturer1@example.com", result.Email);
        }

        [Fact]
        public void GetLecturerByEmail_ShouldReturnLecturer_WhenExists()
        {
            // Arrange
            var lecturer = new LecturerModel { Email = "lecturer1@example.com", Password = "pass1" };
            Database.AddLecturer(lecturer);

            // Act
            var result = Database.GetLecturerByEmail("lecturer1@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("lecturer1@example.com", result.Email);
        }

        [Fact]
        public void ValidateLecturer_ShouldReturnTrue_WhenCredentialsAreValid()
        {
            // Arrange
            var lecturer = new LecturerModel { Email = "lecturer1@example.com", Password = "pass1" };
            Database.AddLecturer(lecturer);

            // Act
            var isValid = Database.ValidateLecturer("lecturer1@example.com", "pass1");

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void LoadManagers_ShouldReturnEmptyList_WhenFileDoesNotExist()
        {
            // Act
            var result = Database.LoadManagers();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void AddAcademicManager_ShouldAddManagerAndSaveToFile()
        {
            // Arrange
            var manager = new AcademicManagerModel { Email = "manager1@example.com", Password = "pass1" };

            // Act
            Database.AddAcademicManager(manager);

            // Assert
            var savedData = File.ReadAllText(_managersPath);
            var deserializedManagers = JsonConvert.DeserializeObject<List<AcademicManagerModel>>(savedData);
            Assert.Single(deserializedManagers);
            Assert.Equal("manager1@example.com", deserializedManagers.First().Email);
        }

        [Fact]
        public void GetAcademicManager_ShouldReturnManager_WhenExists()
        {
            // Arrange
            var manager = new AcademicManagerModel { Email = "manager1@example.com", Password = "pass1" };
            Database.AddAcademicManager(manager);

            // Act
            var result = Database.GetAcademicManager(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("manager1@example.com", result.Email);
        }

        [Fact]
        public void ValidateAcademicManager_ShouldReturnTrue_WhenCredentialsAreValid()
        {
            // Arrange
            var manager = new AcademicManagerModel { Email = "manager1@example.com", Password = "pass1" };
            Database.AddAcademicManager(manager);

            // Act
            var isValid = Database.ValidateAcademicManager("manager1@example.com", "pass1");

            // Assert
            Assert.True(isValid);
        }
    }
}
