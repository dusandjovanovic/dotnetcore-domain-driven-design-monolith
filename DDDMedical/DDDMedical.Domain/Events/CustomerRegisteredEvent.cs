using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events
{
    public class CustomerRegisteredEvent: Event
    {
        public Guid Id;
        public string Name { get; set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }

        public CustomerRegisteredEvent(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
        }
    }
}