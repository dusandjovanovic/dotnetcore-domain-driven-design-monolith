using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.Patient;

namespace DDDMedical.Domain.Commands.Patient
{
    public class RegisterFluPatientCommand : PatientCommand
    {
        public RegisterFluPatientCommand(Guid id, string name, string email, DateTime registrationDate)
        {
            Id = id;
            Name = name;
            Email = email;
            RegistrationDate = registrationDate;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RegisterFluPatientValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}