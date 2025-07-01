using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.Domain
{
    public class ProvinceQuota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 1000)]
        public int DailyQuota { get; set; }  
        public TimeSpan? EndTime { get; set; }
        public int? ProcessingCityId { get; set; }
        public City ProcessingCity { get; set; }
    }
}
