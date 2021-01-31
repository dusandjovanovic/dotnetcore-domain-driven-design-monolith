using DDDMedical.Domain.Commands;

namespace DDDMedical.Domain.Validations
{
    public class RegisterNewCustomerValidation : CustomerValidation<RegisterNewCustomerCommand>
    {
        public RegisterNewCustomerValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}