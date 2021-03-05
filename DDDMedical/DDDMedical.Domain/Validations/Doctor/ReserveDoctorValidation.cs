using DDDMedical.Domain.Commands.Doctor;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.Doctor
{
    public class ReserveDoctorValidation : DoctorValidation<ReserveDoctorCommand>
    {
        public ReserveDoctorValidation()
        {
            ValidateId();
            ValidateReferenceId();
            ValidateReservationDay();
        }
    }
}