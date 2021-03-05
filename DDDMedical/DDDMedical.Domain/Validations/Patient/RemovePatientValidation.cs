using DDDMedical.Domain.Commands.Patient;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.Patient
{
    public class RemovePatientValidation : PatientValidation<RemovePatientCommand>
    {
        public RemovePatientValidation()
        {
            ValidateId();
        }
    }
}