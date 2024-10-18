namespace Contract_Monthly_Claim.Models
{
    public class AcademicManagerModel
    {
        public int ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long PhoneNumber { get; set; } = 0;

        public void ReviewClaim(int claimId)
        {
            // Logic to review a claim
        }

        public void ApproveClaim(int claimId)
        {
            // Logic to approve a claim
        }

        public void RejectClaim(int claimId, string reason)
        {
            // Logic to reject a claim
        }
    }
}
