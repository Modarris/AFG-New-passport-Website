using System.ComponentModel.DataAnnotations;
using AFG_New_passport_Website.Models.Domain;

namespace AFG_New_passport_Website.Models
{
    public class PostAddViewModel
    {
        [Required(ErrorMessage = "آی‌دی پوسته‌خانه لازم است")]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام پوسته‌خانه لازم است")]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;


        [Required(ErrorMessage = "شهر الزامی است")]
        public int CityId { get; set; }
        public List<City>? Cities { get; set; }

 
    }

}
