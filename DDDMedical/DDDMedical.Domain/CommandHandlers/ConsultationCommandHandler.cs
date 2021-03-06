using System;
using System.Threading;
using System.Threading.Tasks;
using DDDMedical.Domain.Commands.Consultation;
using DDDMedical.Domain.Commands.Doctor;
using DDDMedical.Domain.Commands.TreatmentRoom;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Events.Consultation;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using MediatR;

namespace DDDMedical.Domain.CommandHandlers
{
    public class ConsultationCommandHandler : CommandHandler,
        IRequestHandler<RegisterConsultationCommand, bool>
    {
        private readonly IConsultationRepository _consultationRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ITreatmentRoomRepository _treatmentRoomRepository;
        private readonly IMediatorHandler _mediator;
        
        public ConsultationCommandHandler(
            IConsultationRepository consultationRepository,
            IDoctorRepository doctorRepository,
            ITreatmentRoomRepository treatmentRoomRepository,
            IUnitOfWork unitOfWork, 
            IMediatorHandler mediator, 
            INotificationHandler<DomainNotification> notifications) : base(unitOfWork, mediator, notifications)
        {
            _consultationRepository = consultationRepository;
            _doctorRepository = doctorRepository;
            _treatmentRoomRepository = treatmentRoomRepository;
            _mediator = mediator;
        }

        public Task<bool> Handle(RegisterConsultationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var consultation = new Consultation(Guid.NewGuid(), request.DoctorId, request.PatientId, request.TreatmentRoomId, 
                request.RegistrationDate, request.ConsultationDate);
            var treatmentRoom = _treatmentRoomRepository.GetById(request.TreatmentRoomId);

            if (_doctorRepository.IsDoctorReservedByHour(request.DoctorId, request.ConsultationDate))
            {
                _mediator.RaiseEvent(new DomainNotification(request.MessageType, "Doctor's timetable is already taken."));
                return Task.FromResult(false);
            }

            _consultationRepository.Add(consultation);

            if (!Commit()) return Task.FromResult(true);
            
            _mediator.RaiseEvent(new ConsultationRegisteredEvent(consultation.Id, consultation.PatientId, consultation.DoctorId, 
                consultation.TreatmentRoomId, consultation.RegistrationDate, consultation.ConsultationDate));
            _mediator.SendCommand(new ReserveDoctorCommand(consultation.DoctorId, consultation.ConsultationDate, consultation.Id));
            _mediator.SendCommand(new ReserveTreatmentRoomCommand(consultation.TreatmentRoomId,
                consultation.ConsultationDate, treatmentRoom.TreatmentMachineId));

            return Task.FromResult(true);
        }
    }
}