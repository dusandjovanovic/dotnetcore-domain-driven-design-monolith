using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Commands;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Interfaces;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler) notifications;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _mediator.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_unitOfWork.Commit()) return true;

            _mediator.RaiseEvent(new DomainNotification("Commit", "There was a problem while commiting changes."));
            return false;
        }
    }
}