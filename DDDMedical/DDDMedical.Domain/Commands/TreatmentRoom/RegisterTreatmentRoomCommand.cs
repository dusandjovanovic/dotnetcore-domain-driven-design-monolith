using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.TreatmentRoom;

namespace DDDMedical.Domain.Commands.TreatmentRoom
{
    public class RegisterTreatmentRoomCommand : TreatmentRoomCommand
    {
        public RegisterTreatmentRoomCommand(Guid id, Guid treatmentMachineId, string name)
        {
            Id = id;
            TreatmentMachineId = treatmentMachineId;
            Name = name;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RegisterTreatmentRoomValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}