using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.TreatmentRoom;

namespace DDDMedical.Domain.Commands.TreatmentRoom
{
    public class EquipTreatmentRoomWithMachineCommand : TreatmentRoomCommand
    {
        public EquipTreatmentRoomWithMachineCommand(Guid id, Guid treatmentMachineId)
        {
            Id = id;
            TreatmentMachineId = treatmentMachineId;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new EquipTreatmentRoomWithMachineValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}