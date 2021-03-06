using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands.Doctor;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Interfaces;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class DoctorCommandHandler : CommandHandler,
        IRequestHandler<RegisterDoctorCommand, bool>,
        IRequestHandler<RemoveDoctorCommand, bool>,
        IRequestHandler<ReserveDoctorCommand, bool>
    {
        public DoctorCommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications) : base(unitOfWork, mediator, notifications)
        {
        }

        public Task<bool> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Handle(RemoveDoctorCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Handle(ReserveDoctorCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}