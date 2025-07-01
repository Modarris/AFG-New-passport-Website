namespace AFG_New_passport_Website.Models.Domain
{
    public class SearchBlock
    {
        public int Id { get; set; }
        public string IpAddress { get; set; } = null!;
        public int FailedAttempts { get; set; }
        public DateTime LastAttempt { get; set; }
        public DateTime? BlockedUntil { get; set; }
    }
}
