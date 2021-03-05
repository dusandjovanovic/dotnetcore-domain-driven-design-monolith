using System;
using System.Linq;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using DDDMedical.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DDDMedical.Infrastructure.Data.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context) : base(context) { }

        public Doctor GetByEmail(string email)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }

        public bool IsDoctorPulmonologist(Guid doctorId)
        {
            return _dbSet.Find(doctorId).Roles.Contains(Role.Pulmonologist);
        }

        public bool IsDoctorReservedByHour(Guid doctorId, DateTime reservationDate)
        {
            return _dbSet.Find(doctorId).Reservations.TrueForAll(d => d.Date != reservationDate.Date || d.Hour != reservationDate.Hour);
        }
    }
}