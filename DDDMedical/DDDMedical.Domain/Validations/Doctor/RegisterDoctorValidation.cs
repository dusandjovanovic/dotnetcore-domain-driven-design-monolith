using DDDMedical.Domain.Commands.Doctor;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.Doctor
{
    public class RegisterDoctorValidation : DoctorValidation<RegisterDoctorCommand>
    {
        public RegisterDoctorValidation()
        {
            ValidateId();
            ValidateName();
            ValidateEmail();
        }
    }
}