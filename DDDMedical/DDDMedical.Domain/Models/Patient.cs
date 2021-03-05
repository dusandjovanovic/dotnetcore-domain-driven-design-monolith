using System;
using DDDMedical.Domain.Core.Models;

namespace DDDMedical.Domain.Models
{
    public class Patient : EntityAudit
    {
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public DateTime RegistrationDate { get; protected set; }

        protected Patient() {}

        public Patient(Guid id, string name, string email, DateTime registrationDate)
        {
            Id = id;
            Name = name;
            Email = email;
            RegistrationDate = registrationDate;
        }
    }
}