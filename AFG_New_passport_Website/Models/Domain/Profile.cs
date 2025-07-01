using AFG_New_passport_Website.Models.Domain;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFG_New_passport_Website.Models
{
    public class Profile
    {
        [Key]
        public int Profile_Id { get; set; }

        public string Code { get; set; }

        // نام کامل به زبان دری
        [Required(ErrorMessage = "وارد کردن نام ضروری است")]
        [StringLength(50, ErrorMessage = "نام نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string DName { get; set; }

        // نام کامل به زبان انگلیسی
        [Required(ErrorMessage = "وارد کردن نام به زبان انگلیسی ضروری است")]
        [StringLength(50, ErrorMessage = "نام به زبان انگلیسی نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string EName { get; set; }

        // نام خانوادگی به زبان دری
        [Required(ErrorMessage = "وارد کردن نام خانوادگی ضروری است")]
        [StringLength(50, ErrorMessage = "نام خانوادگی نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string DLastName { get; set; }

        // نام خانوادگی به زبان انگلیسی
        [Required(ErrorMessage = "وارد کردن نام خانوادگی به زبان انگلیسی ضروری است")]
        [StringLength(50, ErrorMessage = "نام خانوادگی به زبان انگلیسی نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string ELastName { get; set; }

        // نام پدر به زبان دری
        [Required(ErrorMessage = "وارد کردن نام پدر ضروری است")]
        [StringLength(50, ErrorMessage = "نام پدر نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string DFatherName { get; set; }

        // نام پدر به زبان انگلیسی
        [Required(ErrorMessage = "وارد کردن نام پدر به زبان انگلیسی ضروری است")]
        [StringLength(50, ErrorMessage = "نام پدر به زبان انگلیسی نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string EFatherName { get; set; }

        // نام جد به زبان دری
        [StringLength(50, ErrorMessage = "نام جد نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string DGrandFatherName { get; set; }

        // نام جد به زبان انگلیسی
        [StringLength(50, ErrorMessage = "نام جد به زبان انگلیسی نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string EGrandFatherName { get; set; }

        // تاریخ تولد
        [Required(ErrorMessage = "وارد کردن تاریخ تولد ضروری است")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        // قد
        [Required(ErrorMessage = "وارد کردن اندازه قد تان ضروری است.")]
        [StringLength(3, ErrorMessage = "اندازه قد به سانتی متر نمی‌تواند بیشتر از 3 کاراکتر باشد")]
        [Range(30, 240, ErrorMessage = "قد باید بین ۳۰ تا ۲۴۰ سانتی‌متر باشد.")]
        public string Height { get; set; }

        public String NIDNumber { get; set; }


        // Relation with Gender          
        public int GenderId { get; set; }
        [Required(ErrorMessage = "انتخاب جنسیت ضروری است")]
        public Gender Gender { get; set; }


        //Relation with Profession  
        public int ProfessionId { get; set; }
        [Required(ErrorMessage = "انتخاب شغل ضروری است")]
        public Profession Profession { get; set; }

        // Relation with 


        public Application Application { get; set; }

        public ProfilePhoto ProfilePhoto { get; set; }


        public CompanyInfo? CompanyInfo { get; set; }

        // Identity card
        public int? IdentityDocumentTypeId { get; set; }  // FK
        public IdentityDocumentType? IdentityDocumentType { get; set; }


        //City
        public int? BirthCityId { get; set; }
        public City BirthCity { get; set; }

        public int? ProcessingCityId { get; set; }
        public City ProcessingCity { get; set; }

    }

}
