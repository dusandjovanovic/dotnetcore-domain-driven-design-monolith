using System;
using DDDMedical.Domain.Commands.Abstracts;
using DDDMedical.Domain.Validations.Doctor;

namespace DDDMedical.Domain.Commands.Doctor
{
    public class ReserveDoctorCommand : DoctorCommand
    {
        public ReserveDoctorCommand(Guid id, DateTime reservationDay, Guid referenceId)
        {
            Id = id;
            ReservationDay = reservationDay;
            ReferenceId = referenceId;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new ReserveDoctorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}