using System;
using DDDMedical.Domain.Core.Models;

namespace DDDMedical.Domain.Models
{
    public class Patient : EntityAudit
    {
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public DateTime RegistrationDate { get; protected set; }
        
        public PatientType PatientType { get; protected set; }

        protected Patient() {}

        public Patient(Guid id, string name, string email, DateTime registrationDate, PatientType patientType)
        {
            Id = id;
            Name = name;
            Email = email;
            RegistrationDate = registrationDate;
            PatientType = patientType;
        }
    }
    
    public enum PatientType {
        COVID19 = 0,
        INFLUENZA = 1
    }
}