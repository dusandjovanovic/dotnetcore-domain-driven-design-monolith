using System;
using DDDMedical.Domain.Commands.Abstracts;
using FluentValidation;

namespace DDDMedical.Domain.Validations.Abstracts
{
    public abstract class ConsultationValidation<T> : AbstractValidator<T> where T : ConsultationCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
        }
        
        protected void ValidateDoctorId()
        {
            RuleFor(c => c.DoctorId).NotEqual(Guid.Empty);
        }
        
        protected void ValidatePatientId()
        {
            RuleFor(c => c.PatientId).NotEqual(Guid.Empty);
        }
        
        protected void ValidateTreatmentRoomId()
        {
            RuleFor(c => c.TreatmentRoomId).NotEqual(Guid.Empty);
        }
        
        protected void ValidateRegistrationDate()
        {
            RuleFor(c => c.RegistrationDate).NotEmpty().Must(HaveLaterDateThanNow)
                .WithMessage("Date cannot be in the past.");
        }
        
        protected void ValidateConsultationDate()
        {
            RuleFor(c => c.ConsultationDate).NotEmpty().Must(HaveLaterDateThanNow)
                .WithMessage("Date cannot be in the past.");
        }
        
        private static bool HaveLaterDateThanNow(DateTime date)
        {
            return date >= DateTime.Now;
        }
    }
}