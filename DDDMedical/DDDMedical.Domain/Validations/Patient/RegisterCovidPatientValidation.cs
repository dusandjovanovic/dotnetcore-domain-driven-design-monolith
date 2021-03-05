using DDDMedical.Domain.Commands.Patient;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.Patient
{
    public class RegisterCovidPatientValidation : PatientValidation<RegisterCovidPatientCommand>
    {
        public RegisterCovidPatientValidation()
        {
            ValidateId();
            ValidateName();
            ValidateEmail();
            ValidateRegistrationDate();
        }
    }
}