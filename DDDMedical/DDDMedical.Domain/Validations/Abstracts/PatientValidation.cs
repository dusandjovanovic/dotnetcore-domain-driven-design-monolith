using System;
using DDDMedical.Domain.Commands.Abstracts;
using FluentValidation;

namespace DDDMedical.Domain.Validations.Abstracts
{
    public abstract class PatientValidation<T> : AbstractValidator<T> where T : PatientCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
        }
        
        protected void ValidateName()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.").Length(2, 150)
                .WithMessage("Name must have between 2 and 150 characters.");
        }
        
        protected void ValidateEmail()
        {
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
        }
        
        protected void ValidateRegistrationDate()
        {
            RuleFor(c => c.RegistrationDate).NotEmpty();
        }
    }
}