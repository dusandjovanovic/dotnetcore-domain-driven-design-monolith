using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events
{
    public class CustomerRemovedEvent : Event
    {
        public Guid Id { get; set; }
        
        public CustomerRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}