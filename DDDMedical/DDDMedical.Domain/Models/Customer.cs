using System;
using DDDMedical.Domain.Core.Models;

namespace DDDMedical.Domain.Models
{
    public class Customer : EntityAudit
    {
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        
        protected Customer() {}
        
        public Customer(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }
    }
}