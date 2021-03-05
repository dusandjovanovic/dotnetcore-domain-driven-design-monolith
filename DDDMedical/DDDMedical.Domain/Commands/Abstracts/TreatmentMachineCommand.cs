using System;
using DDDMedical.Domain.Core.Commands;

namespace DDDMedical.Domain.Commands.Abstracts
{
    public abstract class TreatmentMachineCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
    }
}