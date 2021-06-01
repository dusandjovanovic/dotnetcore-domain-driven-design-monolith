using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Models;
using DDDMedical.Domain.Validations.Doctor;

namespace DDDMedical.Domain.Commands.Doctor
{
    public class RegisterDoctorCommand : DoctorCommand
    {
        public RegisterDoctorCommand(Guid id, string name, string email, Role role, List<string> reservations)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
            Reservations = reservations;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RegisterDoctorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}