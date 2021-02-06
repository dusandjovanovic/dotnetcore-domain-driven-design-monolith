using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Infrastructure.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        public void Save<T>(T theEvent) where T : Event
        {
            throw new System.NotImplementedException();
        }
    }
}