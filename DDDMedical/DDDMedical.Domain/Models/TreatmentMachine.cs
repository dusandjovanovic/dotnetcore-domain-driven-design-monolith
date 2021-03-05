using System;
using DDDMedical.Domain.Core.Models;

namespace DDDMedical.Domain.Models
{
    public class TreatmentMachine : EntityAudit
    {
        public string Name { get; protected set; }

        protected TreatmentMachine() {}

        public TreatmentMachine(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}