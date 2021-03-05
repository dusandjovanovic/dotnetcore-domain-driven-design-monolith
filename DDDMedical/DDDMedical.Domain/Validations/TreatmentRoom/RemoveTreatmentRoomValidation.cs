using DDDMedical.Domain.Commands.TreatmentRoom;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.TreatmentRoom
{
    public class RemoveTreatmentRoomValidation : TreatmentRoomValidation<RemoveTreatmentRoomCommand>
    {
        public RemoveTreatmentRoomValidation()
        {
            ValidateId();
        }
    }
}