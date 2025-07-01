using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.Domain
{
    public class Tarifa
    {
        public int Id { get; set; }

        [Required]
        [Range(1000000000000, 9999999999999, ErrorMessage = "شماره حساب باید 13 رقم باشد.")]
        public long CPDAccount { get; set; }

        [Required]
        [Range(1000000000000, 9999999999999, ErrorMessage = "شماره حساب باید 13 رقم باشد.")]
        public long PostAccount { get; set; }

        [Required]
        [Range(1000000000000, 9999999999999, ErrorMessage = "شماره حساب باید 13 رقم باشد.")]
        public long RedCrossAccount { get; set; }

        [Required]
        [Range(0, 99999, ErrorMessage = "مبلغ نباید بیشتر از 5 رقم باشد.")]
        public int PassportPrice { get; set; }

        [Required]
        [Range(0, 99999, ErrorMessage = "مبلغ نباید بیشتر از 5 رقم باشد.")]
        public int PostPayment { get; set; }

        [Required]
        [Range(0, 99999, ErrorMessage = "مبلغ نباید بیشتر از 5 رقم باشد.")]
        public int RedCrossPayment { get; set; }

        // کد تعرفه به صورت رشته است (بدون Range)
        [Required]
        [StringLength(13, ErrorMessage = "کد تعرفه نباید بیشتر از 13 کاراکتر باشد.")]
        public string CPDTarifaCode { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "کد تعرفه نباید بیشتر از 13 کاراکتر باشد.")]
        public string PostTarifaCode { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "کد تعرفه نباید بیشتر از 13 کاراکتر باشد.")]
        public string RedCrossTarifaCode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TarifaDate { get; set; }

        [Required]
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
