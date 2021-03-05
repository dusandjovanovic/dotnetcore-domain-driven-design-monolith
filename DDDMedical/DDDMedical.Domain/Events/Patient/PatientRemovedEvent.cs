using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.Patient
{
    public class PatientRemovedEvent : Event
    {
        public Guid Id { get; private set; }

        public PatientRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}