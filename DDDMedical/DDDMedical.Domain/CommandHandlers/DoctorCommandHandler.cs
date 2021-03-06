using System;
using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands.Doctor;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Events.Doctor;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class DoctorCommandHandler : CommandHandler,
        IRequestHandler<RegisterDoctorCommand, bool>,
        IRequestHandler<RemoveDoctorCommand, bool>,
        IRequestHandler<ReserveDoctorCommand, bool>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMediatorHandler _mediator;
        
        public DoctorCommandHandler(IDoctorRepository doctorRepository,
            IUnitOfWork unitOfWork, 
            IMediatorHandler mediator, 
            INotificationHandler<DomainNotification> notifications) : base(unitOfWork, mediator, notifications)
        {
            _mediator = mediator;
            _doctorRepository = doctorRepository;
        }

        public Task<bool> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var doctor = new Doctor(Guid.NewGuid(), request.Name, request.Email, request.Roles, request.Reservations);

            if (_doctorRepository.GetByEmail(doctor.Email) != null)
            {
                _mediator.RaiseEvent(new DomainNotification(request.MessageType, "E-mail is already registered."));
                return Task.FromResult(false);
            }
            
            _doctorRepository.Add(doctor);

            if (Commit())
            {
                _mediator.RaiseEvent(new DoctorRegisteredEvent(doctor.Id, doctor.Name, doctor.Email, doctor.Roles, doctor.Reservations));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveDoctorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }
            
            _doctorRepository.Remove(request.Id);

            if (Commit())
            {
                _mediator.RaiseEvent(new DoctorRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ReserveDoctorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var reservationDay = request.ReservationDay;
            var existingDoctor = _doctorRepository.GetById(request.Id);

            if (_doctorRepository.IsDoctorReservedByHour(existingDoctor.Id, reservationDay))
            {
                _mediator.RaiseEvent(new DomainNotification(request.MessageType, "Doctor's timetable has already been taken."));
                return Task.FromResult(false);
            }
            
            existingDoctor.Reservations.Add(reservationDay);
            
            _doctorRepository.Update(existingDoctor);

            if (Commit())
            {
                _mediator.RaiseEvent(new DoctorReservedEvent(request.Id, request.ReservationDay, request.ReferenceId));
            }

            return Task.FromResult(true);
        }
    }
}