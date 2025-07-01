using System.ComponentModel.DataAnnotations;
using AFG_New_passport_Website.Models.Domain;

namespace AFG_New_passport_Website.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام محلی شهر لازم است")]
        [StringLength(100, ErrorMessage = "طول نام محلی نباید بیشتر از 100 کاراکتر باشد.")]
        public string NameLocal { get; set; }

        [Required(ErrorMessage = "نام شهر لازم است")]
        [StringLength(100, ErrorMessage = "طول نام شهر نباید بیشتر از 100 کاراکتر باشد.")]
        public string Name { get; set; }

        public bool IsBirthPlace { get; set; } = true;

        public bool IsDocumentProcessPlace { get; set; } = true;



        [Required(ErrorMessage = "کشور الزامی است")]
        public int CountryId { get; set; }
        public List<Country>? Countries { get; set; }
      
    }
}
