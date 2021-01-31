using System;
using DDDMedical.Domain.Validations;

namespace DDDMedical.Domain.Commands
{
    public class RemoveCustomerCommand : CustomerCommand
    {
        public RemoveCustomerCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RemoveCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}