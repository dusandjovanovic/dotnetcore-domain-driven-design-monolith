using System;
using System.Collections.Generic;
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

        public bool IsDoctorReservedByHour(Guid doctorId, DateTime reservationDate)
        {
            List<DateTime> reservations = _dbSet.Find(doctorId).Reservations.Select(r => DateTime.Parse(r).Date).ToList();

            if (reservations.Count == 0)
                return false;
            
            return reservations
                .Any(d => d.Date == reservationDate.Date);
        }

        public bool IsDoctorPulmonologist(Guid doctorId)
        {
            return _dbSet.Find(doctorId).Role == Role.Pulmonologist;
        }

        public bool IsDoctorGeneralPractitioner(Guid doctorId)
        {
            return _dbSet.Find(doctorId).Role == Role.GeneralPractitioner;
        }

        public bool isDoctorReservedInTheFuture(Guid doctorId)
        {
            List<DateTime> reservations = _dbSet.Find(doctorId).Reservations.Select(r => DateTime.Parse(r).Date).ToList();

            if (reservations.Count == 0)
                return false;
            
            return reservations
                .All(d => d.Date > DateTime.Now.Date);
        }
    }
}