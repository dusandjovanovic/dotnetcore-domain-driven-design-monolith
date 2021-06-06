using System;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using DDDMedical.Infrastructure.Data.Context;

namespace DDDMedical.Infrastructure.Data.Repository
{
    public class TreatmentRoomRepository : Repository<TreatmentRoom>, ITreatmentRoomRepository
    {
        public TreatmentRoomRepository(ApplicationDbContext context) : base(context) { }
        
        public bool isTreatmentRoomEquipped(Guid id)
        {
            return _dbSet.Find(id).TreatmentMachineId != Guid.Empty;
        }
    }
}