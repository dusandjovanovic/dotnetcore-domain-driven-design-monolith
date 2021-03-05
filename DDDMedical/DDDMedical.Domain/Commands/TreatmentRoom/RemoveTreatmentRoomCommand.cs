using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.TreatmentRoom;

namespace DDDMedical.Domain.Commands.TreatmentRoom
{
    public class RemoveTreatmentRoomCommand : TreatmentRoomCommand
    {
        public RemoveTreatmentRoomCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RemoveTreatmentRoomValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}