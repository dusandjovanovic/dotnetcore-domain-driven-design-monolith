using System;
using DDDMedical.Domain.Core.Models;

namespace DDDMedical.Domain.Models
{
    public class TreatmentMachine : EntityAudit
    {
        public string Name { get; protected set; }
        
        public TreatmentMachineType TreatmentMachineType { get; protected set; }

        protected TreatmentMachine() {}

        public TreatmentMachine(Guid id, string name, TreatmentMachineType treatmentMachineType)
        {
            Id = id;
            Name = name;
            TreatmentMachineType = treatmentMachineType;
        }
    }

    public enum TreatmentMachineType
    {
        Simple = 0,
        Advanced = 1
    }
}