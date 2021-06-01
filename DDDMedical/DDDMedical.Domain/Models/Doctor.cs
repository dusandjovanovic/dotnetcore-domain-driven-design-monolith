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
        public List<string> Reservations { get; set; }
        
        public Role Role { get; set; }
        
        protected Doctor() {}

        public Doctor(Guid id, string name, string email, Role role, List<string> reservations)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
            Reservations = reservations;
        }
    }
}