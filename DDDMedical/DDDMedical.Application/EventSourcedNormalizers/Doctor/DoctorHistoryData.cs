namespace DDDMedical.Application.EventSourcedNormalizers.Doctor
{
    public class DoctorHistoryData
    {
        public string Id { get; set; }
        public string ReferenceId { get; set; } 
        public string ReservationDay { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string Reservations { get; set; }
        
        public string Action { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}