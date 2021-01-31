using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events
{
    public class CustomerUpdatedEvent : Event
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }

        public CustomerUpdatedEvent(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
        }
    }
}