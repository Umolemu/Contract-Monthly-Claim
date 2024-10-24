namespace Contract_Monthly_Claim.Models
{
    public class ClaimModel
    {
        public int ClaimId { get; set; }
        public int LecturerId { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
        public TimeSpan HoursWorked { get; set; }
        public double TotalAmount { get; set; }
        public string Description { get; set; }
        public string? DocumentPath { get; set; }
        public string ClaimStatus { get; set; } = "Pending";
        public DateTime ApprovalDate { get; set; } = DateTime.MinValue;
        public string? RejectionReason { get; set; }
    }
}
