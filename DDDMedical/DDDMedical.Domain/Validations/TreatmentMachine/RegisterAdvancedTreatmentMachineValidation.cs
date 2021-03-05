using DDDMedical.Domain.Commands.TreatmentMachine;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.TreatmentMachine
{
    public class RegisterAdvancedTreatmentMachineValidation : TreatmentMachineValidation<RegisterAdvancedTreatmentMachineCommand>
    {
        public RegisterAdvancedTreatmentMachineValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}