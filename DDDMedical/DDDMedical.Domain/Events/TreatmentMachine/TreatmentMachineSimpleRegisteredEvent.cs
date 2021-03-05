using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.TreatmentMachine
{
    public class TreatmentMachineSimpleRegisteredEvent : Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        
        public TreatmentMachineSimpleRegisteredEvent(Guid id, string name)
        {
            Id = id;
            AggregateId = id;
            Name = name;
        }
    }
}