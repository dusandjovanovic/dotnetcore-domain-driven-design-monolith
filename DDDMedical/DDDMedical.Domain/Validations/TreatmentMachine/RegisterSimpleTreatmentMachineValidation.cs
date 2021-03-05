using DDDMedical.Domain.Commands.TreatmentMachine;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.TreatmentMachine
{
    public class RegisterSimpleTreatmentMachineValidation : TreatmentMachineValidation<RegisterSimpleTreatmentMachineCommand>
    {
        public RegisterSimpleTreatmentMachineValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}