namespace DDDMedical.Application.EventSourcedNormalizers.Consultation
{
    public class ConsultationHistoryData
    {
        public string Id { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string TreatmentRoomId { get; set; }
        public string RegistrationDate { get; set; }
        public string ConsultationDate { get; set; }
        
        public string Action { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}