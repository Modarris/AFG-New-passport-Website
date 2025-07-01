using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.Domain
{
    public class ProfilePhoto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PhotoPath { get; set; } // یا Base64 یا آدرس فایل

        public DateTime UploadDate { get; set; } = DateTime.Now;

        // Foreign Key
        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
    }
}
