using DDDMedical.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDMedical.Infrastructure.Data.Mappings
{
    public class ConsultationMap : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");
            
            builder.Property(c => c.DoctorId)
                .HasColumnName("DoctorId");
            
            builder.Property(c => c.PatientId)
                .HasColumnName("PatientId");
            
            builder.Property(c => c.TreatmentRoomId)
                .HasColumnName("TreatmentRoomId");

            builder.Property(c => c.ConsultationDate)
                .HasColumnName("ConsultationDate")
                .IsRequired();

            builder.Property(c => c.RegistrationDate)
                .HasColumnType("Date")
                .IsRequired();
            
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}