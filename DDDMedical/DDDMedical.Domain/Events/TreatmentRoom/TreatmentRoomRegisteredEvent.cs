using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.TreatmentRoom
{
    public class TreatmentRoomRegisteredEvent : Event
    {
        public Guid Id { get; private set; }
        public Guid TreatmentMachineId { get; private set; }
        public string Name { get; private set; }
        
        public TreatmentRoomRegisteredEvent(Guid id, Guid treatmentMachineId, string name)
        {
            Id = id;
            AggregateId = id;
            TreatmentMachineId = treatmentMachineId;
            Name = name;
        }
    }
}