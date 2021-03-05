using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.Doctor;

namespace DDDMedical.Domain.Commands.Doctor
{
    public class RemoveDoctorCommand : DoctorCommand
    {
        public RemoveDoctorCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RemoveDoctorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}