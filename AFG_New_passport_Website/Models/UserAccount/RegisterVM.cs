using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.UserAccount
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "لطفاً نام خود را وارد کنید")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفاً ایمیل خود را وارد کنید")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "رمز عبور باید حداقل ۶ حرف باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفاً تأیید رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور مطابقت ندارد")]
        public string ConfirmPassword { get; set; }
    }
}
