using System;
using DDDMedical.Domain.Core.Models;

namespace DDDMedical.Domain.Models
{
    public class TreatmentRoom : EntityAudit
    {
        public Guid TreatmentMachineId { get; protected set; }
        public string Name { get; protected set; }
        
        protected TreatmentRoom() {}

        public TreatmentRoom(Guid id, Guid treatmentMachineId, string name)
        {
            Id = id;
            TreatmentMachineId = treatmentMachineId;
            Name = name;
        }
    }
}