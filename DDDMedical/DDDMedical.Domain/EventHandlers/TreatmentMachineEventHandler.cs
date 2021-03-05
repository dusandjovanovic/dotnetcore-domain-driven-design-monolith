using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Events.TreatmentMachine;
using MediatR;

namespace DDDMedical.Domain.EventHandlers
{
    public class TreatmentMachineEventHandler :
        INotificationHandler<TreatmentMachineSimpleRegisteredEvent>,
        INotificationHandler<TreatmentMachineAdvancedRegisteredEvent>
    {
        public Task Handle(TreatmentMachineSimpleRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(TreatmentMachineAdvancedRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}