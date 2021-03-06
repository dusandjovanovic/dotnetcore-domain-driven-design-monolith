using DDDMedical.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDMedical.Infrastructure.Data.Mappings
{
    public class TreatmentMachineMap : IEntityTypeConfiguration<TreatmentMachine>
    {
        public void Configure(EntityTypeBuilder<TreatmentMachine> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}