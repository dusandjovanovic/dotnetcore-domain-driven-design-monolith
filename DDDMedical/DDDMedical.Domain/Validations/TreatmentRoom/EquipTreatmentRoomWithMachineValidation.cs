using DDDMedical.Domain.Commands.TreatmentRoom;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.TreatmentRoom
{
    public class EquipTreatmentRoomWithMachineValidation : TreatmentRoomValidation<EquipTreatmentRoomWithMachineCommand>
    {
        public EquipTreatmentRoomWithMachineValidation()
        {
            ValidateId();
            ValidateTreatmentMachineId();
        }
    }
}