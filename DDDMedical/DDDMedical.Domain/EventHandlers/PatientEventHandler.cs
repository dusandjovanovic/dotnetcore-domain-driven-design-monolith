using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Events.Patient;
using MediatR;

namespace DDDMedical.Domain.EventHandlers
{
    public class PatientEventHandler: 
        INotificationHandler<PatientCovidRegisteredEvent>,
        INotificationHandler<PatientFluRegisteredEvent>,
        INotificationHandler<PatientRemovedEvent>
    {
        public Task Handle(PatientCovidRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PatientFluRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PatientRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}