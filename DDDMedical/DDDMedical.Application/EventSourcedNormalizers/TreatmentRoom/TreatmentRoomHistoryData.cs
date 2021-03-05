namespace DDDMedical.Application.EventSourcedNormalizers.TreatmentRoom
{
    public class TreatmentRoomHistoryData
    {
        public string Id { get; set; }
        public string TreatmentMachineId { get; set; }
        public string Name { get; set; }
        public string ReservationDay { get; set; }
        
        public string Action { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}