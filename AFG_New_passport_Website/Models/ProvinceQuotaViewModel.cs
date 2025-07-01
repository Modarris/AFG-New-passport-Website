using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models
{
    public class ProvinceQuotaViewModel
    {
        [Required(ErrorMessage = "لطفاً ولایت را انتخاب کنید")]
        public int SelectedCityId { get; set; }
        public TimeSpan? EndTime { get; set; }
        [Required(ErrorMessage = "لطفاً تعداد نوبت را وارد کنید")]
        [Range(1, 1000, ErrorMessage = "تعداد نوبت باید بین 1 تا 1000 باشد")]
        public int DailyQuota { get; set; }
    }

}
