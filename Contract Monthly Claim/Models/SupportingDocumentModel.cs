namespace Contract_Monthly_Claim.Models
{
    public class SupportingDocumentModel
    {
        public int DocumentId { get; set; }
        public int ClaimId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedDate { get; set; }
       
    }
}
