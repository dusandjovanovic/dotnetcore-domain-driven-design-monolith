using DDDMedical.Domain.Commands.Doctor;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.Doctor
{
    public class RemoveDoctorValidation : DoctorValidation<RemoveDoctorCommand>
    {
        public RemoveDoctorValidation()
        {
            ValidateId();
        }
    }
}