namespace Contract_Monthly_Claim.Models
{
    public class SupportingDocumentModel
    {
        public int DocumentId { get; set; }
        public int ClaimId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedDate { get; set; }

        public void UploadDocument(string filePath)
        {
            // Logic to upload document
        }

        public SupportingDocumentModel GetDocumentDetails(int documentId)
        {
            // Logic to get document details
        }
    }
}
