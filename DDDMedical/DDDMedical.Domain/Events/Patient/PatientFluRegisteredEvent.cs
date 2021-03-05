using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.Patient
{
    public class PatientFluRegisteredEvent : Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        
        public PatientFluRegisteredEvent(Guid id, string name, string email, DateTime registrationDate)
        {
            Id = id;
            AggregateId = id;
            Name = name;
            Email = email;
            RegistrationDate = registrationDate;
        }
    }
}