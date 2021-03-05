using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.TreatmentRoom
{
    public class TreatmentRoomReservedEvent : Event
    {
        public Guid Id { get; private set; }
        public DateTime ReservationDay { get; private set; }
        public Guid TreatmentMachineId { get; private set; }
        
        public TreatmentRoomReservedEvent(Guid id, DateTime reservationDay, Guid treatmentMachineId)
        {
            Id = id;
            AggregateId = id;
            TreatmentMachineId = treatmentMachineId;
            ReservationDay = reservationDay;
        }
    }
}