using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands.TreatmentMachine;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Interfaces;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class TreatmentMachineCommandHandler : CommandHandler,
        IRequestHandler<RegisterSimpleTreatmentMachineCommand, bool>,
        IRequestHandler<RegisterAdvancedTreatmentMachineCommand, bool>
    {
        public TreatmentMachineCommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications) : base(unitOfWork, mediator, notifications)
        {
        }

        public Task<bool> Handle(RegisterSimpleTreatmentMachineCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Handle(RegisterAdvancedTreatmentMachineCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}