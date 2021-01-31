using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
    }
}