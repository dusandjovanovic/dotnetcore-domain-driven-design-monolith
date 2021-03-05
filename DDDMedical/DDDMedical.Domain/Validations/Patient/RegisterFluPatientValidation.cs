using DDDMedical.Domain.Commands.Patient;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.Patient
{
    public class RegisterFluPatientValidation : PatientValidation<RegisterFluPatientCommand>
    {
        public RegisterFluPatientValidation()
        {
            ValidateId();
            ValidateName();
            ValidateEmail();
            ValidateRegistrationDate();
        }
    }
}