using System;
using DDDMedical.Domain.Commands.Abstracts;
using FluentValidation;

namespace DDDMedical.Domain.Validations.Abstracts
{
    public abstract class TreatmentMachineValidation<T> : AbstractValidator<T> where T : TreatmentMachineCommand
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
    }
}