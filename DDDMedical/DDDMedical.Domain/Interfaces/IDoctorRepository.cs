using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Doctor GetByEmail(string email);
    }
}