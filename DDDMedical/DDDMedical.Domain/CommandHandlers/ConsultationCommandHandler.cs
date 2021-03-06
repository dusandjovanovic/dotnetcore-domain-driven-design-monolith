using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands.Consultation;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Interfaces;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class ConsultationCommandHandler : CommandHandler,
        IRequestHandler<RegisterConsultationCommand, bool>
    {
        public ConsultationCommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications) : base(unitOfWork, mediator, notifications)
        {
        }

        public Task<bool> Handle(RegisterConsultationCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}