using System;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Events.Consultation
{
    public class ConsultationRegisteredEvent : Event
    {
        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public Guid TreatmentRoomId { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public DateTime ConsultationDate { get; private set; }

        public ConsultationRegisteredEvent(Guid id, Guid patientId, Guid doctorId, Guid treatmentRoomId,
            DateTime registrationDate, DateTime consultationDate)
        {
            Id = id;
            AggregateId = id;
            PatientId = patientId;
            DoctorId = doctorId;
            TreatmentRoomId = treatmentRoomId;
            RegistrationDate = registrationDate;
            ConsultationDate = consultationDate;
        }
    }
}