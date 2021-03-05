namespace DDDMedical.Application.EventSourcedNormalizers.Patient
{
    public class PatientHistoryData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RegistrationDate { get; set; }
        
        public string Action { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}