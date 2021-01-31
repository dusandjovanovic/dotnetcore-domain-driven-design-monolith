using DDDMedical.Domain.Commands;

namespace DDDMedical.Domain.Validations
{
    public class RemoveCustomerValidation : CustomerValidation<RemoveCustomerCommand>
    {
        public RemoveCustomerValidation()
        {
            ValidateId();
        } 
    }
}