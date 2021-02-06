using System.Linq;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using DDDMedical.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DDDMedical.Infrastructure.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context) { }

        public Customer GetByEmail(string email)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}