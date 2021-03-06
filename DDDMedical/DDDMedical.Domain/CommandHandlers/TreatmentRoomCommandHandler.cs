using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands.TreatmentRoom;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Interfaces;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class TreatmentRoomCommandHandler : CommandHandler,
    IRequestHandler<EquipTreatmentRoomWithMachineCommand, bool>,
    IRequestHandler<RegisterTreatmentRoomCommand, bool>,
    IRequestHandler<RemoveTreatmentRoomCommand, bool>,
    IRequestHandler<ReserveTreatmentRoomCommand, bool>
    {
        public TreatmentRoomCommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications) : base(unitOfWork, mediator, notifications)
        {
        }

        public Task<bool> Handle(EquipTreatmentRoomWithMachineCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Handle(RegisterTreatmentRoomCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Handle(RemoveTreatmentRoomCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Handle(ReserveTreatmentRoomCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}