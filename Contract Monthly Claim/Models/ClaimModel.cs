namespace Contract_Monthly_Claim.Models
{
    public class ClaimModel
    {
        public int ClaimId { get; set; }
        public int LecturerId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public TimeSpan HoursWorked { get; set; }
        public double TotalAmount { get; set; }
        public string ClaimStatus { get; set; }
        public DateTime CoordinatorApprovalDate { get; set; }
        public int ManagerApprovalDate { get; set; }

        public double CalculateTotalAmount()
        {
            return 0.0;
        }

        public bool SubmitClaim()
        {
            // Logic for submitting the claim
        }

        public void UpdateClaimStatus(string status)
        {
            ClaimStatus = status;
        }

        public void AddSupportingDocument(SupportingDocumentModel document)
        {
            // Logic to add a supporting document
        }

        public ClaimModel GetClaimDetails(int claimId)
        {
            // Logic to retrieve claim details
        }
    }
}
