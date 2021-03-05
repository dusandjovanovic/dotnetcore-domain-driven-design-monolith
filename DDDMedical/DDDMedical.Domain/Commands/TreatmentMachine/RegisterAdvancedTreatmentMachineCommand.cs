using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.TreatmentMachine;

namespace DDDMedical.Domain.Commands.TreatmentMachine
{
    public class RegisterAdvancedTreatmentMachineCommand : TreatmentMachineCommand
    {
        public RegisterAdvancedTreatmentMachineCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RegisterAdvancedTreatmentMachineValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}