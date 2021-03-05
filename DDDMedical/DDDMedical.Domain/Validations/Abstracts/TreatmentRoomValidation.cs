using System;
using DDDMedical.Domain.Commands.Abstracts;
using FluentValidation;

namespace DDDMedical.Domain.Validations.Abstracts
{
    public abstract class TreatmentRoomValidation<T> : AbstractValidator<T> where T : TreatmentRoomCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
        }
        
        protected void ValidateTreatmentMachineId()
        {
            RuleFor(c => c.TreatmentMachineId).NotEqual(Guid.Empty);
        }
        
        protected void ValidateName()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.").Length(2, 150)
                .WithMessage("Name must have between 2 and 150 characters.");
        }
        
        protected void ValidateReservationDay()
        {
            RuleFor(c => c.ReservationDay).NotEmpty().Must(HaveLaterDateThanNow)
                .WithMessage("Date cannot be in the past.");
        }
        
        private static bool HaveLaterDateThanNow(DateTime date)
        {
            return date >= DateTime.Now;
        }
    }
}