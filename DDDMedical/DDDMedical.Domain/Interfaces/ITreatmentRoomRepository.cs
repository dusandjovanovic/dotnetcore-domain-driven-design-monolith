using System;
using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Interfaces
{
    public interface ITreatmentRoomRepository : IRepository<TreatmentRoom>
    {
        public bool isTreatmentRoomEquipped(Guid id);
    }
}