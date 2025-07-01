
using AFG_New_passport_Website.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using AppoinmentType = AFG_New_passport_Website.Models.Domain.AppoinmentType;
using Application = AFG_New_passport_Website.Models.Domain.Application;
using Microsoft.AspNetCore.Mvc.Rendering;
using City = AFG_New_passport_Website.Models.Domain.City;
using System.ComponentModel.DataAnnotations;



namespace AFG_New_passport_Website.Models
{
    public class ViewModel
    {
        // Profile
        public int Profile_Id { get; set; }
        public string Code { get; set; }
        public string DName { get; set; }
        public string EName { get; set; }
        public string DLastName { get; set; }
        public string ELastName { get; set; }
        public string DFatherName { get; set; }
        public string EFatherName { get; set; }
        public string DGrandFatherName { get; set; }
        public string EGrandFatherName { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime DateOfBirth { get; set; }
        public string Height { get; set; }

        /*public IFormFile? ProfilePicture { get; set; }*/
        [NotMapped]
        public string BirthDateShamsi { get; set; }

        // NID Number
        public string NIDNumber { get; set; }


        //Address
        public int Address_Id { get; set; }

        public string PostalAddress { get; set; } // PostalAddress

        public string AddressDetails { get; set; } // AddressDetails

        [RegularExpression(@"^\d{9}$", ErrorMessage = "شماره باید فقط شامل ۹ رقم باشد.")]
        public string PrimaryPhone { get; set; }

        public string? SecondaryPhone { get; set; } // شماره فرعی (اختیاری)
        public List<UserAddress> UserAddresses { get; set; }
        public UserAddress UserAddress { get; set; }


        // Gender
        [Required(ErrorMessage = "لطفاً جنسیت را انتخاب کنید.")]
        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک جنسیت معتبر انتخاب کنید.")]
        public int GenderId { get; set; }
        public string GenderDName { get; set; }
        public string GenderEName { get; set; }
        public List<Gender>? Genders { get; set; }
        public Gender Gender { get; set; }

        // Profession
        [Required(ErrorMessage = "لطفاً شغل را انتخاب کنید.")]
        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک شغل معتبر انتخاب کنید.")]
        public int ProfessionId { get; set; }
        public string ProfessionNameLocal{ get; set; }
        public string ProfessionName{ get; set; }
        public List<SelectListItem> AllProfessions { get; set; }
        public Profession Profession { get; set; }



        //Postal Address 
        [Required(ErrorMessage = "لطفاً پوسته خانه را انتخاب کنید.")]
        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک پوسته خانه معتبر انتخاب کنید.")]
        public int PostalAddressId { get; set; }
        public string PostalAddressName { get; set; }
        public List<PostalAddress>? PostalAddresses { get; set; }


  
        //---------------------------------------------------------------------
        public IFormFile ProfilePhoto { get; set; }
        public string ProfilePhotoBase64 { get; set; }
        //---------------------------------------------------------------------
        // حالا اضافه کن:
        public int SelectedPassportTypeId { get; set; }
        public int SelectedPassportDurationId { get; set; }
        public int SelectedPaymentTypeId { get; set; }
        public int SelectedAppoinmentTypeId { get; set; }


        // همچنین لیست‌ها برای dropdown
        public List<PassportType> PassportTypes { get; set; }
        public List<PassportDuration> PassportDurations { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
        public List<AppoinmentType> AppoinmentTypes { get; set; }


        public PassportType PassportType { get; set; }
        public PassportDuration PassportDuration { get; set; }
        public PaymentType PaymentType { get; set; }
        public AppoinmentType AppoinmentType { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public Profile Profile { get; set; }

        //---------------------------------------------------------------------
       /* public IFormFile ProfilePhoto { get; set; }*/


        //Company Info
        public string? Position { get; set; }
        public string? LicenseNumber { get; set; }
        public string? TIN { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public string? CompanyAddress { get; set; }

        public CompanyInfo? CompanyInfo { get; set; }


        //Identity Document Types
        public int IdentityDocumentTypeId { get; set; }
        public string Name { get; set; }
        public IdentityDocumentType? IdentityDocumentType { get; set; }
        public List<IdentityDocumentType> IdentityDocumentTypes { get; set; }

            //City
        public int SelectedBirthCityId { get; set; }
        public List<City> AllCitiesForBirth { get; set; }
        public string BirthCityNameLocal { get; set; }
        public int SelectedProcessingCityId { get; set; }          
       public List<City> ActiveCitiesForProcessing { get; set; }
        public string ProcessingCityNameLocal { get; set; }
        

        //Bank account
        public int BankAccounsId { get; set; }
        public BankAccounts BankAccounts { get; set; }
        public long CPDAccount { get; set; }

        public string FixedAmount { get; set; }

     
        public string BarcodeImageBase64 { get; set; }

        public DateTime? AppointmentDate { get; set; }
    }

    }