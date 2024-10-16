namespace Contract_Monthly_Claim.Models
{
    public class LecturerModel
    {
        public int LecturerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public double HourlyRate { get; set; }

        public void SubmitClaim(TimeSpan hoursWorked, SupportingDocumentModel[] documents)
        {
            // Logic to submit a claim
        }

        public void UploadDocument(SupportingDocumentModel document)
        {
            // Logic to upload a document
        }

        public void ViewClaimStatus(int claimId)
        {
            // Logic to view claim status
        }
    }
}
