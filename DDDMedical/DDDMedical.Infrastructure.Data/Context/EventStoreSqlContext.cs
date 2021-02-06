using DDDMedical.Domain.Core.Events;
using DDDMedical.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DDDMedical.Infrastructure.Data.Context
{
    public class EventStoreSqlContext : DbContext
    {
        public EventStoreSqlContext(DbContextOptions<EventStoreSqlContext> options) : base(options) {}
        
        public DbSet<StoredEvent> StoredEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}