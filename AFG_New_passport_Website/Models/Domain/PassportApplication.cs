namespace AFG_New_passport_Website.Models.Domain
{
    public class PassportApplication
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
