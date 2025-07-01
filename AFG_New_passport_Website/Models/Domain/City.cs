using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.Domain
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NameLocal { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsBirthPlace { get; set; } = true;  
        public bool IsDocumentProcessPlace { get; set; } = true;

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<PostalAddress>? PostalAddresses { get; set; }


    }
}
