namespace AFG_New_passport_Website.Models.Domain
{
    public class Application
    {
        public int Id { get; set; }


        //AppoinmentTypeId
        public int AppoinmentTypeId { get; set; }
        public AppoinmentType AppoinmentType { get; set; }



        //PassportTypeId
        public int PassportTypeId { get; set; }
        public PassportType PassportType { get; set; }


        //PassportDurationId
        public int PassportDurationId { get; set; }
        public PassportDuration PassportDuration { get; set; }


        //PaymentTypeId
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }


        //Profile_Id
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        //Address 
        public int UserAddressId { get; set; }
        public UserAddress UserAddress { get; set; }

        // Tarifa
        public ICollection<Tarifa> Tarifas { get; set; }

    }
}
