using System;
using DDDMedical.Domain.Core.Commands;

namespace DDDMedical.Domain.Commands.Abstracts
{
    public abstract class PatientCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public DateTime RegistrationDate { get; protected set; }
    }
}