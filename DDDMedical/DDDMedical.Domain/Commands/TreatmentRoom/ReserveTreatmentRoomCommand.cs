using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.TreatmentRoom;

namespace DDDMedical.Domain.Commands.TreatmentRoom
{
    public class ReserveTreatmentRoomCommand : TreatmentRoomCommand
    {
        public ReserveTreatmentRoomCommand(Guid id, DateTime reservationDay, Guid treatmentMachineId)
        {
            Id = id;
            TreatmentMachineId = treatmentMachineId;
            ReservationDay = reservationDay;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new ReserveTreatmentRoomValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}