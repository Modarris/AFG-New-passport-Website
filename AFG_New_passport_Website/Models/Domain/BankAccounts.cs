using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.Domain
{
    public class BankAccounts
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "شماره حساب ضروری است")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "شماره حساب باید دقیقاً 13 رقم باشد")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "شماره حساب فقط باید شامل ارقام باشد")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "نام دارنده حساب ضروری است")]
        public string AccountHolder { get; set; }

        public int? Fee { get; set; }

        public int? ProcessingCityId { get; set; }
        public City ProcessingCity { get; set; }

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
