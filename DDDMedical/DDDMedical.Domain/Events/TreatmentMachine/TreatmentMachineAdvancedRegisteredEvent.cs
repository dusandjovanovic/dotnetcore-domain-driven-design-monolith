using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.TreatmentMachine
{
    public class TreatmentMachineAdvancedRegisteredEvent : Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        
        public TreatmentMachineAdvancedRegisteredEvent(Guid id, string name)
        {
            Id = id;
            AggregateId = id;
            Name = name;
        }
    }
}