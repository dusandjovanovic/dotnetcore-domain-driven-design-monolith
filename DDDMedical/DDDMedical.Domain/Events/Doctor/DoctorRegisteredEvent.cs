using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using DDDMedical.Domain.Core.Events;
using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Events.Doctor
{
    public class DoctorRegisteredEvent : Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public ImmutableList<Role> Roles { get; private set; }
        public List<DateTime> Reservations { get; private set; }
        
        public DoctorRegisteredEvent(Guid id, string name, string email, ImmutableList<Role> roles, List<DateTime> reservations)
        {
            Id = id;
            AggregateId = id;
            Name = name;
            Email = email;
            Roles = roles;
            Reservations = reservations;
        }
    }
}