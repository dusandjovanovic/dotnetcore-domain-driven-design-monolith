using System;
using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands.TreatmentMachine;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Events.TreatmentMachine;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class TreatmentMachineCommandHandler : CommandHandler,
        IRequestHandler<RegisterSimpleTreatmentMachineCommand, bool>,
        IRequestHandler<RegisterAdvancedTreatmentMachineCommand, bool>
    {
        private readonly ITreatmentMachineRepository _treatmentMachineRepository;
        private readonly IMediatorHandler _mediator;

        public TreatmentMachineCommandHandler(
            ITreatmentMachineRepository treatmentMachineRepository,
            IUnitOfWork unitOfWork, 
            IMediatorHandler mediator, 
            INotificationHandler<DomainNotification> notifications) : base(unitOfWork, mediator, notifications)
        {
            _mediator = mediator;
            _treatmentMachineRepository = treatmentMachineRepository;
        }

        public Task<bool> Handle(RegisterSimpleTreatmentMachineCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var treatmentMachine = new TreatmentMachine(Guid.NewGuid(), request.Name, TreatmentMachineType.Simple);
            
            _treatmentMachineRepository.Add(treatmentMachine);

            if (Commit())
            {
                _mediator.RaiseEvent(new TreatmentMachineSimpleRegisteredEvent(treatmentMachine.Id, treatmentMachine.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RegisterAdvancedTreatmentMachineCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var treatmentMachine = new TreatmentMachine(Guid.NewGuid(), request.Name, TreatmentMachineType.Advanced);
            
            _treatmentMachineRepository.Add(treatmentMachine);

            if (Commit())
            {
                _mediator.RaiseEvent(new TreatmentMachineAdvancedRegisteredEvent(treatmentMachine.Id, treatmentMachine.Name));
            }

            return Task.FromResult(true);
        }
    }
}