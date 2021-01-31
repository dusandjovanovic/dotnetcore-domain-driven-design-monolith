using DDDMedical.Domain.Commands;

namespace DDDMedical.Domain.Validations
{
    public class UpdateCustomerValidation : CustomerValidation<UpdateCustomerCommand>
    {
        public UpdateCustomerValidation()
        {
            ValidateId();
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}