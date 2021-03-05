using System;
using DDDMedical.Domain.Core.Models;

namespace DDDMedical.Domain.Models
{
    public class Consultation : EntityAudit
    {
        public Guid DoctorId { get; protected set; }
        public Guid PatientId { get; protected set; }
        public Guid TreatmentRoomId { get; protected set; }
        public DateTime RegistrationDate { get; protected set; }
        public DateTime ConsultationDate { get; protected set; }

        protected Consultation() {}

        public Consultation(Guid id, Guid doctorId, Guid patientId, Guid treatmentRoomId, DateTime registrationDate, DateTime consultationDate)
        {
            Id = id;
            DoctorId = doctorId;
            PatientId = patientId;
            TreatmentRoomId = treatmentRoomId;
            RegistrationDate = registrationDate;
            ConsultationDate = consultationDate;
        }
    }
}