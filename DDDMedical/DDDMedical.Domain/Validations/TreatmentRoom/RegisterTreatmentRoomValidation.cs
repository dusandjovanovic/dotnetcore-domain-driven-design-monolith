using DDDMedical.Domain.Commands.TreatmentRoom;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.TreatmentRoom
{
    public class RegisterTreatmentRoomValidation : TreatmentRoomValidation<RegisterTreatmentRoomCommand>
    {
        public RegisterTreatmentRoomValidation()
        {
            ValidateId();
            ValidateTreatmentMachineId();
            ValidateName();
        }
    }
}