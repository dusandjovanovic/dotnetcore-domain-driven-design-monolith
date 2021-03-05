using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.TreatmentRoom
{
    public class TreatmentRoomRemovedEvent : Event
    {
        public Guid Id { get; private set; }
        
        public TreatmentRoomRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}