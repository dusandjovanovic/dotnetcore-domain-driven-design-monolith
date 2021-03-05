using System;
using DDDMedical.Domain.Core.Commands;

namespace DDDMedical.Domain.Commands.Abstracts
{
    public abstract class TreatmentRoomCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid TreatmentMachineId { get; protected set; }
        public string Name { get; protected set; }
        public DateTime ReservationDay { get; protected set; }
    }
}