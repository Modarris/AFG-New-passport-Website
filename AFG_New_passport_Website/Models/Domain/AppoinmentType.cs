using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.Domain
{
    public class AppoinmentType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Type { get; set; }


    }
}
