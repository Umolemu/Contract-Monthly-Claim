using Contract_Monthly_Claim.Models;
using Contract_Monthly_Claim.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Moq;

namespace Contract_Monthly_Claim.Tests
{
    public class DatabaseTests
    {
        private readonly string _testLecturersJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "test_lecturers.json");

        // This setup runs before each test
        public DatabaseTests()
        {
            // Ensure the test JSON file is clean before each test
            File.WriteAllText(_testLecturersJsonPath, "[]");
        }

        [Fact]
        public void AddLecturer_ShouldAddLecturerToJsonFile()
        {
            // Arrange
            var lecturer = new LecturerModel { FirstName = "Test", LastName = "User", Email = "test@example.com", Password = "password" };

            // Act
            Database.AddLecturer(lecturer);

            // Assert
            var lecturers = LoadLecturersFromFile();
            Assert.Single(lecturers);
            Assert.Equal("Test", lecturers[0].FirstName);
        }

        [Fact]
        public void GetLecturerByEmail_ShouldReturnLecturer_WhenExists()
        {
            // Arrange
            var lecturer = new LecturerModel { FirstName = "Test", LastName = "User", Email = "test@example.com", Password = "password" };
            Database.AddLecturer(lecturer);

            // Act
            var result = Database.GetLecturerByEmail("test@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.FirstName);
        }

        [Fact]
        public void GetLecturerByEmail_ShouldReturnNull_WhenDoesNotExist()
        {
            // Act
            var result = Database.GetLecturerByEmail("nonexistent@example.com");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ValidateLecturer_ShouldReturnTrue_WhenValidCredentials()
        {
            // Arrange
            var lecturer = new LecturerModel { FirstName = "Test", LastName = "User", Email = "test@example.com", Password = "password" };
            Database.AddLecturer(lecturer);

            // Act
            var result = Database.ValidateLecturer("test@example.com", "password");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateLecturer_ShouldReturnFalse_WhenInvalidPassword()
        {
            // Arrange
            var lecturer = new LecturerModel { FirstName = "Test", LastName = "User", Email = "test@example.com", Password = "password" };
            Database.AddLecturer(lecturer);

            // Act
            var result = Database.ValidateLecturer("test@example.com", "wrongpassword");

            // Assert
            Assert.False(result);
        }

        private List<LecturerModel> LoadLecturersFromFile()
        {
            var jsonData = File.ReadAllText(_testLecturersJsonPath);
            return JsonConvert.DeserializeObject<List<LecturerModel>>(jsonData);
        }
    }
}
