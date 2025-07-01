namespace AFG_New_passport_Website.Models.Domain
{
    public class Gender
    {
        public int Id { get; set; }
        public string DName { get; set; } // Male and Female
        public string EName { get; set; } // Male and Female

        // Optional:Relation
        public ICollection<Profile>? Profiles { get; set; }
    }
}
