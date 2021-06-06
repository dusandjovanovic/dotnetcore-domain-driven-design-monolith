using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Events.Consultation;
using MediatR;

namespace DDDMedical.Domain.EventHandlers
{
    public class ConsultationEventHandler : INotificationHandler<ConsultationRegisteredEvent>
    {
        public Task Handle(ConsultationRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}