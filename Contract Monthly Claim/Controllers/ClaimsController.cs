using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Contract_Monthly_Claim.Models;
using Contract_Monthly_Claim.Data;

namespace Contract_Monthly_Claim.Controllers
{
    [Route("Claims")]
    public class ClaimsController : Controller
    {
        private readonly string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "claims.json");

        // Create new claim
        [HttpPost("Create")]
        public IActionResult Create(ClaimModel claim)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                var claims = LoadClaims();

                claim.ClaimId = claims.Count > 0 ? claims[^1].ClaimId + 1 : 1;

                // Handle file upload 
                var file = Request.Form.Files["Document"];

                if (file != null && file.Length > 0)
                {
                    var allowedExtensions = new[] { ".txt", ".pdf", ".docx", ".xlsx" };
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("Document", "Invalid file type. Only .txt, .pdf, .docx, and .xlsx files are allowed.");
                        return View(claim);
                    }

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    var fileName = Guid.NewGuid().ToString() + "" + fileExtension;
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    claim.DocumentPath = fileName;

                    claims.Add(claim);

                    SaveClaims(claims);              

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {                 

                    ModelState.AddModelError("Document", "Please upload a document.");
                }
            }

            return View("~/Views/Dashboard/Create.cshtml", claim);
        }


        // Process a claim: approve or reject
        [HttpPost("Process")]
        public IActionResult ProcessClaim(int claimId, string action, string reason = "")
        {
            // Assume manager is logged in
            var manager = new AcademicManagerModel();

            // Check if the claim exists
            var claims = LoadClaims();
            var claim = claims.FirstOrDefault(claim => claim.ClaimId == claimId);

            if (claim == null)
            {
                return NotFound("Claim not found.");
            }

            if (action == "approve")
            {
                // Approve the claim
                claim.ApprovalDate = DateTime.Now;
                claim.ClaimStatus = "Approved";
                manager.ApproveClaim(claimId);
            }
            else if (action == "reject")
            {
                // Reject the claim with reason
                claim.RejectionReason = reason;
                claim.ClaimStatus = "Rejected";
                manager.RejectClaim(claimId, reason);
            }

            // Save the updated claims back to the JSON file
            SaveClaims(claims);

            return RedirectToAction("Index", "Dashboard");
        }

        // View all claims
        [HttpGet("View")]
        public IActionResult ViewClaims()
        {
            var claims = LoadClaims();
            return View(claims);
        }

        // Load claims from the JSON file
        private List<ClaimModel> LoadClaims()
        {
            if (!System.IO.File.Exists(jsonFilePath))
            {
                return new List<ClaimModel>();
            }

            var json = System.IO.File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<List<ClaimModel>>(json) ?? new List<ClaimModel>();
        }

        [HttpGet("Download/{fileName}")]
        public IActionResult Download(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var contentType = "application/octet-stream";

            return PhysicalFile(filePath, contentType, fileName);
        }

        // Save claims to the JSON file
        private void SaveClaims(List<ClaimModel> claims)
        {
            var json = JsonSerializer.Serialize(claims, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(jsonFilePath, json);
        }
    }
}
