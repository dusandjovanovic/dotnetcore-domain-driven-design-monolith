using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using DDDMedical.Infrastructure.Data.Context;

namespace DDDMedical.Infrastructure.Data.Repository
{
    public class TreatmentRoomRepository : Repository<TreatmentRoom>, ITreatmentRoomRepository
    {
        public TreatmentRoomRepository(ApplicationDbContext context) : base(context) { }
    }
}