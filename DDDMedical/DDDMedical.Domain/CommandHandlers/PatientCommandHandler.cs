using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands.Patient;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Interfaces;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class PatientCommandHandler : CommandHandler,
        IRequestHandler<RegisterCovidPatientCommand, bool>,
        IRequestHandler<RegisterFluPatientCommand, bool>,
        IRequestHandler<RemovePatientCommand, bool>
    {
        public PatientCommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications) : base(unitOfWork, mediator, notifications)
        {
        }

        public Task<bool> Handle(RegisterCovidPatientCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Handle(RegisterFluPatientCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Handle(RemovePatientCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}