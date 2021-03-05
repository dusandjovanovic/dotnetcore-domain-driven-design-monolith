using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.Doctor
{
    public class DoctorRemovedEvent : Event
    {
        public Guid Id { get; private set; }

        public DoctorRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}