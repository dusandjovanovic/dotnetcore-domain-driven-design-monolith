using System;
using System.Collections.Generic;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Infrastructure.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);

        IList<StoredEvent> All(Guid aggregateId);
    }
}