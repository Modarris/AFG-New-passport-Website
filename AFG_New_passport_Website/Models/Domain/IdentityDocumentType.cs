using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.Domain
{
    public class IdentityDocumentType
    {
        public int Id { get; set; }


        [Required]
        [StringLength(100)]
        public string Name { get; set; } 

        public ICollection<Profile>? Profiles { get; set; }
    }
}
