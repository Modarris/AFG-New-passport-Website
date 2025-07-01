using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.Domain
{
    public class PublicHoliday
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime HolidayDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
