using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Contract_Monthly_Claim.Models
{
    public class ProgrammeCoordinatorModel
    {
        public int CoordinatorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long PhoneNumber { get; set; } = 0;

        private readonly string claimsJson = Path.Combine(Directory.GetCurrentDirectory(), "Data", "claims.json");

        // Review Claim
        public ClaimModel ReviewClaim(int claimId)
        {
            var claims = GetClaimsFromFile();
            var selectedClaim = claims.FirstOrDefault(claim => claim.ClaimId == claimId);
            return selectedClaim;
        }

        // Approve Claim
        public void ApproveClaim(int claimId)
        {
            var claims = GetClaimsFromFile();

            var selectedClaim = claims.FirstOrDefault(claim => claim.ClaimId == claimId);
            if (selectedClaim != null)
            {
                selectedClaim.ClaimStatus = "Approved";
                selectedClaim.ApprovalDate = DateTime.Now;

                SaveClaimsToFile(claims);
            }
        }

        // Reject Claim
        public void RejectClaim(int claimId, string reason)
        {
            var claims = GetClaimsFromFile();
            var selectedClaim = claims.FirstOrDefault(claim => claim.ClaimId == claimId);
            if (selectedClaim != null)
            {
                selectedClaim.ClaimStatus = "Rejected";
                selectedClaim.RejectionReason = reason;

                SaveClaimsToFile(claims);
            }
        }

        // Helper: Read claims from JSON file
        private List<ClaimModel> GetClaimsFromFile()
        {
            if (!File.Exists(claimsJson))
                return new List<ClaimModel>();

            var json = File.ReadAllText(claimsJson);
            return JsonSerializer.Deserialize<List<ClaimModel>>(json);
        }

        // Helper: Save claims to JSON file
        private void SaveClaimsToFile(List<ClaimModel> claims)
        {
            var updatedJson = JsonSerializer.Serialize(claims);
            File.WriteAllText(claimsJson, updatedJson);
        }
    }
}

