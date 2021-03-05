using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Events;
using MediatR;

namespace DDDMedical.Domain.EventHandlers
{
    public class ConsultationEventHandler : INotificationHandler<CustomerRegisteredEvent>
    {
        public Task Handle(CustomerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}