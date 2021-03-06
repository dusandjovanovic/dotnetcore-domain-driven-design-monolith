using System;
using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands.TreatmentRoom;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Events.Patient;
using DDDMedical.Domain.Events.TreatmentRoom;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class TreatmentRoomCommandHandler : CommandHandler,
    IRequestHandler<EquipTreatmentRoomWithMachineCommand, bool>,
    IRequestHandler<RegisterTreatmentRoomCommand, bool>,
    IRequestHandler<RemoveTreatmentRoomCommand, bool>,
    IRequestHandler<ReserveTreatmentRoomCommand, bool>
    {
        private readonly ITreatmentRoomRepository _treatmentRoomRepository;
        private readonly ITreatmentMachineRepository _treatmentMachineRepository;
        private readonly IMediatorHandler _mediator;
        
        public TreatmentRoomCommandHandler(
            ITreatmentRoomRepository treatmentRoomRepository,
            ITreatmentMachineRepository treatmentMachineRepository,
            IUnitOfWork unitOfWork, 
            IMediatorHandler mediator, 
            INotificationHandler<DomainNotification> notifications) : base(unitOfWork, mediator, notifications)
        {
            _mediator = mediator;
            _treatmentRoomRepository = treatmentRoomRepository;
            _treatmentMachineRepository = treatmentMachineRepository;
        }

        public Task<bool> Handle(EquipTreatmentRoomWithMachineCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }
            
            var existingTreatmentRoom = _treatmentRoomRepository.GetById(request.Id);
            var treatmentRoom = new TreatmentRoom(existingTreatmentRoom.Id, request.TreatmentMachineId,
                existingTreatmentRoom.Name);

            _treatmentRoomRepository.Update(treatmentRoom);

            if (Commit())
            {
                _mediator.RaiseEvent(new TreatmentRoomEquippedWithMachineEvent(existingTreatmentRoom.Id, existingTreatmentRoom.TreatmentMachineId));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RegisterTreatmentRoomCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var treatmentRoom = new TreatmentRoom(Guid.NewGuid(), request.TreatmentMachineId, request.Name);

            _treatmentRoomRepository.Add(treatmentRoom);

            if (Commit())
            {
                _mediator.RaiseEvent(new TreatmentRoomRegisteredEvent(treatmentRoom.Id, treatmentRoom.TreatmentMachineId, treatmentRoom.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveTreatmentRoomCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }
            
            _treatmentRoomRepository.Remove(request.Id);

            if (Commit())
            {
                _mediator.RaiseEvent(new TreatmentRoomRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ReserveTreatmentRoomCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }
            
            var existingTreatmentRoom = _treatmentRoomRepository.GetById(request.Id);
            var treatmentRoom = new TreatmentRoom(existingTreatmentRoom.Id, request.TreatmentMachineId,
                existingTreatmentRoom.Name);

            _treatmentRoomRepository.Update(treatmentRoom);

            if (Commit())
            {
                _mediator.RaiseEvent(new TreatmentRoomReservedEvent(existingTreatmentRoom.Id, request.ReservationDay, existingTreatmentRoom.TreatmentMachineId));
            }

            return Task.FromResult(true);
        }
    }
}