using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Contract_Monthly_Claim.Models;

namespace Contract_Monthly_Claim.Controllers
{
    [Route("Claims")]
    public class ClaimsController : Controller
    {
        private readonly string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "claims.json");

        // Action to Create a new claim
        [HttpPost("Create")]
        public IActionResult Create(ClaimModel claim)
        {
            if (ModelState.IsValid)
            {
                // Load existing claims from the JSON file
                var claims = LoadClaims();

                // Assign a new ClaimId and add the new claim
                claim.ClaimId = claims.Count > 0 ? claims[^1].ClaimId + 1 : 1;
                claims.Add(claim);

                // Save updated claims back to the JSON file
                SaveClaims(claims);

                return RedirectToAction("Dashboard", "Home"); // Redirect to the view page to see the claims
            }

            return View(claim); // Return to the view with validation messages if the claim is invalid
        }

        // Action to View all claims
        [HttpGet("View")]
        public IActionResult ViewClaims()
        {
            var claims = LoadClaims(); // Load claims from the JSON file
            return View(claims); // Pass the claims to the view
        }

        // Helper method to load claims from the JSON file
        private List<ClaimModel> LoadClaims()
        {
            if (!System.IO.File.Exists(jsonFilePath))
            {
                return new List<ClaimModel>();
            }

            var json = System.IO.File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<List<ClaimModel>>(json) ?? new List<ClaimModel>();
        }

        // Helper method to save claims to the JSON file
        private void SaveClaims(List<ClaimModel> claims)
        {
            var json = JsonSerializer.Serialize(claims, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(jsonFilePath, json);
        }
    }
}
