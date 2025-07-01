using AFG_New_passport_Website.Models.Domain;

namespace AFG_New_passport_Website.Models
{
    public class ProfileViewModel
    {
        public Profile Profile { get; set; }
        public UserAddress UserAddress { get; set; }
        public Gender Gender { get; set; }
        public Profession Profession { get; set; }

        public PostalAddress PostalAddress { get; set; }
        public PassportType PassportType { get; set; }
        public PassportDuration PassportDuration { get; set; }
        public PaymentType PaymentType { get; set; }
        public AppoinmentType AppoinmentType { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public string BirthDateShamsi { get; set; }
        public ProfilePhoto ProfilePhoto { get; set; }
        public IdentityDocumentType? IdentityDocumentType { get; set; }

        // City selection and display
        public int? SelectedBirthCityId { get; set; }
        public int? SelectedProcessingCityId { get; set; }

        public City BirthCity { get; set; }           // فقط شهرهای مجاز برای محل تولد
        public City ProcessingCity { get; set; }

        public int BankAccounsId { get; set; }

        public List<BankAccounts> BankAccounts { get; set; } = new();
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public string CamercialBankAccount { get; set; }
        public string OrgCode { get; set; }
        public string LocationCode { get; set; }
        public string UniteCode { get; set; }
        public string PassportRevenueCode { get; set; }
        public string PassportBookletRevenueCode { get; set; }

        // Tarifa Codes 

        public ICollection<Tarifa> Tarifas { get; set; } = new List<Tarifa>();


        public Tarifa FirstTarifa => Tarifas?.FirstOrDefault();


        public string CPDTarifaCode => FirstTarifa?.CPDTarifaCode;
        public string PostTarifaCode => FirstTarifa?.PostTarifaCode;
        public string RedCrossTarifaCode => FirstTarifa?.RedCrossTarifaCode;
        public int? PassportPrice { get; set; }

        public string Code { get; set; }
        public string BarcodeImageBase64 { get; set; }
      


        public DateTime? AppointmentDate { get; set; }
    
        public Appointment Appointment { get; set; }
    }





}



