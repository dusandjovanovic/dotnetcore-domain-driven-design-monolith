using System;
using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Events;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class CustomerCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCustomerCommand, bool>,
        IRequestHandler<UpdateCustomerCommand, bool>,
        IRequestHandler<RemoveCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler _mediator;

        public CustomerCommandHandler(ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications) 
            : base(unitOfWork, mediator, notifications)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public Task<bool> Handle(RegisterNewCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var customer = new Customer(Guid.NewGuid(), request.Name, request.Email, request.BirthDate);

            if (_customerRepository.GetByEmail(customer.Email) != null)
            {
                _mediator.RaiseEvent(new DomainNotification(request.MessageType, "E-mail is already registered."));
                return Task.FromResult(false);
            }
            
            _customerRepository.Add(customer);

            if (Commit())
            {
                _mediator.RaiseEvent(new CustomerRegisteredEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var customer = new Customer(request.Id, request.Name, request.Email, request.BirthDate);
            var existingCustomer = _customerRepository.GetByEmail(customer.Email);

            if (existingCustomer != null && existingCustomer.Id != customer.Id)
            {
                if (!existingCustomer.Equals(customer))
                {
                    _mediator.RaiseEvent(new DomainNotification(request.MessageType, "E-mail has already been taken."));
                    return Task.FromResult(false);
                }
            }
            
            _customerRepository.Update(customer);

            if (Commit())
            {
                _mediator.RaiseEvent(new CustomerUpdatedEvent(customer.Id, customer.Name, customer.Email,
                    customer.BirthDate));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }
            
            _customerRepository.Remove(request.Id);

            if (Commit())
            {
                _mediator.RaiseEvent(new CustomerRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _customerRepository.Dispose();
        }
    }
}