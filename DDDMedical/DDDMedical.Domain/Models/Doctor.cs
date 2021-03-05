using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using DDDMedical.Domain.Core.Models;

namespace DDDMedical.Domain.Models
{
    public class Doctor : EntityAudit
    {
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public ImmutableList<Role> Roles { get; protected set; }
        public List<DateTime> Reservations { get; set; }
        
        protected Doctor() {}

        public Doctor(Guid id, string name, string email, ImmutableList<Role> roles, List<DateTime> reservations)
        {
            Id = id;
            Name = name;
            Email = email;
            Roles = roles;
            Reservations = reservations;
        }
    }
}