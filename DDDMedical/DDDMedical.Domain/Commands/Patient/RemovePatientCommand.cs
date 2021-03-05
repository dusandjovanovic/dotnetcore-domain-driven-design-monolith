using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.Patient;

namespace DDDMedical.Domain.Commands.Patient
{
    public class RemovePatientCommand : PatientCommand
    {
        public RemovePatientCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RemovePatientValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}