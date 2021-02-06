using System;
using System.Linq;
using System.Collections.Generic;
using DDDMedical.Domain.Core.Events;
using DDDMedical.Infrastructure.Data.Context;

namespace DDDMedical.Infrastructure.Data.Repository.EventSourcing
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreSqlContext _context;

        public EventStoreRepository(EventStoreSqlContext context)
        {
            _context = context;
        }
        
        
        public IList<StoredEvent> All(Guid aggregateId)
        {
            return (from e in _context.StoredEvents where e.AggregateId == aggregateId select e).ToList();
        }
        
        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvents.Add(theEvent);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}