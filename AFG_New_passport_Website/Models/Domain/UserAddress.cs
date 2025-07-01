using System.Net;
using System.ComponentModel.DataAnnotations;
namespace AFG_New_passport_Website.Models.Domain
{
    public class UserAddress
    {
        [Key]
        public int Address_Id { get; set; }

        [Required(ErrorMessage = "وارد کردن جزئیات آدرس ضروری است")]
        [StringLength(150, ErrorMessage = "حداکثر 300 کاراکتر مجاز است")]
        public string AddressDetails { get; set; }

        [Required(ErrorMessage = "وارد کردن شماره تماس اصلی ضروری است")]
        [Phone(ErrorMessage = "شماره تماس اصلی معتبر نیست")]
        [RegularExpression(@"^\+93\d{9}$", ErrorMessage = "شماره تلفن باید با +93 شروع شده و ۹ رقم داشته باشد.")]
        public string PrimaryPhone { get; set; }

        [Phone(ErrorMessage = "شماره تماس فرعی معتبر نیست")]
        public string? SecondaryPhone { get; set; }


        // Profile relation
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }


        //Postal address relation
        public int PostalAddressId { get; set; }
        public PostalAddress PostalAddress { get; set; }

        public ICollection<Application> Applications { get; set; }



    }
}
