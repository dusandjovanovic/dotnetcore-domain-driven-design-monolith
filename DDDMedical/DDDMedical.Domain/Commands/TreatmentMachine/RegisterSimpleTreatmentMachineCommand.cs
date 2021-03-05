using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.TreatmentMachine;

namespace DDDMedical.Domain.Commands.TreatmentMachine
{
    public class RegisterSimpleTreatmentMachineCommand : TreatmentMachineCommand
    {
        public RegisterSimpleTreatmentMachineCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RegisterSimpleTreatmentMachineValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}