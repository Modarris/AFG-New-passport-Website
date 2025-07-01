namespace AFG_New_passport_Website.Models.Domain
{
    public class Country
    {
        public int Id { get; set; }
        public string DName { get; set; }
        public string EName { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Location>? Locations { get; set; }
        public ICollection<City>? Cities { get; set; }

    }
}
