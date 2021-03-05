using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.Doctor
{
    public class DoctorReservedEvent : Event
    {
        public Guid Id { get; private set; }
        public DateTime ReservationDay { get; private set; }
        public Guid ReferenceId { get; private set; }
        
        public DoctorReservedEvent(Guid id, DateTime reservationDay, Guid referenceId)
        {
            Id = id;
            AggregateId = id;
            ReservationDay = reservationDay;
            ReferenceId = referenceId;
        }
    }
}