using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Core.Models;
using DDDMedical.Domain.Models;
using DDDMedical.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DDDMedical.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<TreatmentMachine> TreatmentMachines { get; set; }
        public DbSet<TreatmentRoom> TreatmentRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConsultationMap());
            modelBuilder.ApplyConfiguration(new DoctorMap());
            modelBuilder.ApplyConfiguration(new PatientMap());
            modelBuilder.ApplyConfiguration(new TreatmentMachineMap());
            modelBuilder.ApplyConfiguration(new TreatmentRoomMap());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries()
                .Where(x => x.Entity is EntityAudit)
                .ToList();
            UpdateSoftDelete(entries);
            UpdateTimestamps(entries);
        }

        private void UpdateSoftDelete(List<EntityEntry> entries)
        {
            var filtered = entries
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Deleted);

            foreach (var entry in filtered)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ((EntityAudit) entry.Entity).IsDeleted = false;
                        break;
                    case EntityState.Deleted:
                        ((EntityAudit) entry.Entity).IsDeleted = true;
                        break;
                }
            }
        }
        
        private void UpdateTimestamps(List<EntityEntry> entries)
        {
            var filtered = entries
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            var currentUserId = 1;

            foreach (var entry in filtered)
            {
                if (entry.State == EntityState.Added)
                {
                    ((EntityAudit) entry.Entity).CreatedAt = DateTime.UtcNow;
                    ((EntityAudit) entry.Entity).CreatedBy = currentUserId;
                }
                ((EntityAudit) entry.Entity).UpdatedAt = DateTime.UtcNow;
                ((EntityAudit) entry.Entity).UpdatedBy = currentUserId;
            }
        }
    }
}