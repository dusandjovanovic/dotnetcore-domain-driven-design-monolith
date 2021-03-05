using System.Linq;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using DDDMedical.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DDDMedical.Infrastructure.Data.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context) : base(context) { }

        public Doctor GetByEmail(string email)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}