using System;
using DDDMedical.Domain.Commands;
using FluentValidation;

namespace DDDMedical.Domain.Validations
{
    public abstract class CustomerValidation<T> : AbstractValidator<T> where T : CustomerCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.").Length(2, 150)
                .WithMessage("Name must have between 2 and 150 characters.");
        }

        protected void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate).NotEmpty().Must(HaveMinimumAge)
                .WithMessage("Birt date must be 18 years or older.");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
        }

        private static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}