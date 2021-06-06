using System;
using System.Linq;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using DDDMedical.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DDDMedical.Infrastructure.Data.Repository
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context) { }

        public Patient GetByEmail(string email)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }

        public bool isPatientCovid19(Guid patientId)
        {
            return _dbSet.Find(patientId).PatientType == PatientType.COVID19;
        }

        public bool isPatientInfluenza(Guid patientId)
        {
            return _dbSet.Find(patientId).PatientType == PatientType.INFLUENZA;
        }
    }
}