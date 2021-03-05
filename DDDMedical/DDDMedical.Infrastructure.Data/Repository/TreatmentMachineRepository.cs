using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using DDDMedical.Infrastructure.Data.Context;

namespace DDDMedical.Infrastructure.Data.Repository
{
    public class TreatmentMachineRepository : Repository<TreatmentMachine>, ITreatmentMachineRepository
    {
        public TreatmentMachineRepository(ApplicationDbContext context) : base(context) { }
        
        
    }
}