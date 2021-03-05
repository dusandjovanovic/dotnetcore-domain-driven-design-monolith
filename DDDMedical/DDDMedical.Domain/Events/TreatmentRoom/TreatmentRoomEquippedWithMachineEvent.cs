using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.TreatmentRoom
{
    public class TreatmentRoomEquippedWithMachineEvent : Event
    {
        public Guid Id { get; private set; }
        public Guid TreatmentMachineId { get; private set; }
        
        public TreatmentRoomEquippedWithMachineEvent(Guid id, Guid treatmentMachineId)
        {
            Id = id;
            AggregateId = id;
            TreatmentMachineId = treatmentMachineId;
        }
    }
}