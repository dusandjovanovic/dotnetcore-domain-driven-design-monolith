using DDDMedical.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDMedical.Infrastructure.Data.Mappings
{
    public class DoctorMap : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");
            
            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(c => c.Roles)
                .HasColumnName("Roles")
                .IsRequired();
            
            builder.Property(c => c.Reservations)
                .HasColumnName("Reservations")
                .IsRequired();
            
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}