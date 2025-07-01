using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.Domain
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime SlotDate { get; set; }

        [Required]
        public int AppointmentNumber { get; set; }

        [Required]
        public int? ProcessingCityId { get; set; }
        public City ProcessingCity { get; set; }

        [Required]
        public int ProfileId { get; set; }   
        public Profile Profile { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }

}
