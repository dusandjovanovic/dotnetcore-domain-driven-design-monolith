using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Events.TreatmentRoom;
using MediatR;

namespace DDDMedical.Domain.EventHandlers
{
    public class TreatmentRoomEventHandler :
        INotificationHandler<TreatmentRoomRegisteredEvent>,
        INotificationHandler<TreatmentRoomReservedEvent>,
        INotificationHandler<TreatmentRoomRemovedEvent>,
        INotificationHandler<TreatmentRoomEquippedWithMachineEvent>
    {
        public Task Handle(TreatmentRoomRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(TreatmentRoomReservedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(TreatmentRoomRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(TreatmentRoomEquippedWithMachineEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}