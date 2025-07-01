namespace AFG_New_passport_Website.Models.Domain
{
    public class CompanyInfo
    {
        public int Id { get; set; }

        public int ProfileId { get; set; } 
        public Profile Profile { get; set; }

        public string? Position { get; set; }
        public string? LicenseNumber { get; set; }
        public string? TIN { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public string? CompanyAddress { get; set; }
    }
}
