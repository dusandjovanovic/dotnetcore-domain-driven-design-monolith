using DDDMedical.Domain.Commands.Consultation;
using DDDMedical.Domain.Validations.Abstracts;

namespace DDDMedical.Domain.Validations.Consultation
{
    public class RegisterConsultationValidation : ConsultationValidation<RegisterConsultationCommand>
    {
        public RegisterConsultationValidation()
        {
            ValidateId();
            ValidateDoctorId();
            ValidatePatientId();
            ValidateTreatmentRoomId();
            ValidateRegistrationDate();
            ValidateConsultationDate();
        }
    }
}