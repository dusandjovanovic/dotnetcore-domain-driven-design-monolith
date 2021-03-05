using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.Consultation;

namespace DDDMedical.Domain.Commands.Consultation
{
    public class RegisterConsultationCommand : ConsultationCommand
    {
        public RegisterConsultationCommand(Guid id, Guid patientId, Guid doctorId, Guid treatmentRoomId,
            DateTime registrationDate, DateTime consultationDate)
        {
            Id = id;
            PatientId = patientId;
            DoctorId = doctorId;
            TreatmentRoomId = treatmentRoomId;
            RegistrationDate = registrationDate;
            ConsultationDate = consultationDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterConsultationValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}