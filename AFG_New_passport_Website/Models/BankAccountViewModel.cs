using System.ComponentModel.DataAnnotations;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AFG_New_passport_Website.Models
{
    public class BankAccountViewModel
    {

        public int SelectedCityId { get; set; }
        [Required(ErrorMessage = "شماره حساب ضروری است")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "شماره حساب باید دقیقاً 13 رقم باشد")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "شماره حساب فقط باید شامل ارقام باشد")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "نام دارنده حساب ضروری است")]
        public string AccountHolder { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "فیس باید عدد مثبت باشد")]
        public int? Fee { get; set; }

        [Required(ErrorMessage = "کد ارگان ضروری است")]
        public string OrgCode { get; set; }

        [Required(ErrorMessage = "کد موقعیت ضروری است")]
        public string LocationCode { get; set; }

        [Required(ErrorMessage = "کد واحد ضروری است")]
        public string UniteCode { get; set; }

        [Required(ErrorMessage = "کد عواید پاسپورت ضروری است")]
        public string PassportRevenueCode { get; set; }

        public string? PassportBookletRevenueCode { get; set; }


        [StringLength(13, MinimumLength = 13, ErrorMessage = "شماره حساب باید دقیقاً 13 رقم باشد")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "شماره حساب فقط باید شامل ارقام باشد")]
        public string? CamercialBankAccount { get; set; }
    }
}

