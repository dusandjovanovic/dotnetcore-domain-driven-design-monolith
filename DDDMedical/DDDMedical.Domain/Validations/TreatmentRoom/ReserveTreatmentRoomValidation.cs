using DDDMedical.Domain.Commands.TreatmentRoom;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.TreatmentRoom
{
    public class ReserveTreatmentRoomValidation : TreatmentRoomValidation<ReserveTreatmentRoomCommand>
    {
        public ReserveTreatmentRoomValidation()
        {
            ValidateId();
            ValidateTreatmentMachineId();
            ValidateReservationDay();
        }
    }
}