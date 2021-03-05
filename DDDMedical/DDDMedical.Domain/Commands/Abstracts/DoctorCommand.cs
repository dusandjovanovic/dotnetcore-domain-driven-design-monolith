using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using DDDMedical.Domain.Core.Commands;
using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Commands.Abstracts
{
    public abstract class DoctorCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid ReferenceId { get; protected set; } 
        public DateTime ReservationDay { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public ImmutableList<Role> Roles { get; protected set; }
        public List<DateTime> Reservations { get; set; }
    }
}