using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Events.Doctor;
using MediatR;

namespace DDDMedical.Domain.EventHandlers
{
    public class DoctorEventHandler : 
        INotificationHandler<DoctorRegisteredEvent>, 
        INotificationHandler<DoctorReservedEvent>, 
        INotificationHandler<DoctorRemovedEvent>
    {
        public Task Handle(DoctorRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(DoctorReservedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(DoctorRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}