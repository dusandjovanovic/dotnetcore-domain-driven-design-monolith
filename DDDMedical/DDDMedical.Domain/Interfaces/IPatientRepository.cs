using System;
using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Patient GetByEmail(string email);
        
        bool isPatientCovid19(Guid patientId);
        
        bool isPatientInfluenza(Guid patientId);
    }
}