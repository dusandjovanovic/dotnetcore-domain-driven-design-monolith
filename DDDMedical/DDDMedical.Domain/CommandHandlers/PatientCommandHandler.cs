using System;
using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands.Patient;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Events.Patient;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class PatientCommandHandler : CommandHandler,
        IRequestHandler<RegisterCovidPatientCommand, bool>,
        IRequestHandler<RegisterFluPatientCommand, bool>,
        IRequestHandler<RemovePatientCommand, bool>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMediatorHandler _mediator;
        
        public PatientCommandHandler(
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork, 
            IMediatorHandler mediator, 
            INotificationHandler<DomainNotification> notifications) : base(unitOfWork, mediator, notifications)
        {
            _mediator = mediator;
            _patientRepository = patientRepository;
        }

        public Task<bool> Handle(RegisterCovidPatientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var patient = new Patient(Guid.NewGuid(), request.Name, request.Email, request.RegistrationDate, PatientType.COVID19);

            if (_patientRepository.GetByEmail(patient.Email) != null)
            {
                _mediator.RaiseEvent(new DomainNotification(request.MessageType, "E-mail is already registered."));
                return Task.FromResult(false);
            }
            
            _patientRepository.Add(patient);

            if (Commit())
            {
                _mediator.RaiseEvent(new PatientCovidRegisteredEvent(patient.Id, patient.Name, patient.Email, patient.RegistrationDate));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RegisterFluPatientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var patient = new Patient(Guid.NewGuid(), request.Name, request.Email, request.RegistrationDate, PatientType.INFLUENZA);

            if (_patientRepository.GetByEmail(patient.Email) != null)
            {
                _mediator.RaiseEvent(new DomainNotification(request.MessageType, "E-mail is already registered."));
                return Task.FromResult(false);
            }
            
            _patientRepository.Add(patient);

            if (Commit())
            {
                _mediator.RaiseEvent(new PatientFluRegisteredEvent(patient.Id, patient.Name, patient.Email, patient.RegistrationDate));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemovePatientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }
            
            _patientRepository.Remove(request.Id);

            if (Commit())
            {
                _mediator.RaiseEvent(new PatientRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }
    }
}