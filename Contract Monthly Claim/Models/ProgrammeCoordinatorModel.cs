namespace Contract_Monthly_Claim.Models
{
    public class ProgrammeCoordinatorModel
    {
        public int CoordinatorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }

        public void VerifyClaim(int claimId)
        {
            // Logic to verify a claim
        }

        public void RejectClaim(int claimId, string reason)
        {
            // Logic to reject a claim
        }

        public void ApproveClaim(int claimId)
        {
            // Logic to approve a claim
        }
    }
}
