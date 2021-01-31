using System;
using DDDMedical.Domain.Validations;

namespace DDDMedical.Domain.Commands
{
    public class RegisterNewCustomerCommand : CustomerCommand
    {
        public RegisterNewCustomerCommand(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RegisterNewCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}