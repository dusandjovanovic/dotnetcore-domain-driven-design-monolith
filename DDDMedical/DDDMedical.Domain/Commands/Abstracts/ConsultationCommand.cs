using System;
using DDDMedical.Domain.Core.Commands;

namespace DDDMedical.Domain.Commands.Abstracts
{
    public abstract class ConsultationCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid DoctorId { get; protected set; }
        public Guid PatientId { get; protected set; }
        public Guid TreatmentRoomId { get; protected set; }
        public DateTime RegistrationDate { get; protected set; }
        public DateTime ConsultationDate { get; protected set; }
    }
}