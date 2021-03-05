namespace DDDMedical.Application.EventSourcedNormalizers.TreatmentMachine
{
    public class TreatmentMachineHistoryData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public string Action { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}