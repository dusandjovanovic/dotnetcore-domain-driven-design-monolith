using DDDMedial.Infrastructure.Bus;
using DDDMedical.Application.Interfaces;
using DDDMedical.Application.Services;
using DDDMedical.Domain.CommandHandlers;
using DDDMedical.Domain.Commands.Consultation;
using DDDMedical.Domain.Commands.Doctor;
using DDDMedical.Domain.Commands.Patient;
using DDDMedical.Domain.Commands.TreatmentMachine;
using DDDMedical.Domain.Commands.TreatmentRoom;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Events;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.EventHandlers;
using DDDMedical.Domain.Events.Consultation;
using DDDMedical.Domain.Events.Doctor;
using DDDMedical.Domain.Events.Patient;
using DDDMedical.Domain.Events.TreatmentMachine;
using DDDMedical.Domain.Events.TreatmentRoom;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Services;
using DDDMedical.Infrastructure.Data.EventSourcing;
using DDDMedical.Infrastructure.Data.Repository;
using DDDMedical.Infrastructure.Data.Repository.EventSourcing;
using DDDMedical.Infrastructure.Data.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DDDMedical.API
{
    public class Injector
    {
          public static void RegisterServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped<IConsultationService, ConsultationService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<ITreatmentRoomService, TreatmentRoomService>();
            services.AddScoped<ITreatmentMachineService, TreatmentMachineService>();

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            
            services.AddScoped<INotificationHandler<ConsultationRegisteredEvent>, ConsultationEventHandler>();
            
            services.AddScoped<INotificationHandler<DoctorRegisteredEvent>, DoctorEventHandler>();
            services.AddScoped<INotificationHandler<DoctorRemovedEvent>, DoctorEventHandler>();
            services.AddScoped<INotificationHandler<DoctorReservedEvent>, DoctorEventHandler>();
            
            services.AddScoped<INotificationHandler<PatientCovidRegisteredEvent>, PatientEventHandler>();
            services.AddScoped<INotificationHandler<PatientFluRegisteredEvent>, PatientEventHandler>();
            services.AddScoped<INotificationHandler<PatientRemovedEvent>, PatientEventHandler>();

            services.AddScoped<INotificationHandler<TreatmentMachineAdvancedRegisteredEvent>, TreatmentMachineEventHandler>();
            services.AddScoped<INotificationHandler<TreatmentMachineSimpleRegisteredEvent>, TreatmentMachineEventHandler>();

            services.AddScoped<INotificationHandler<TreatmentRoomEquippedWithMachineEvent>, TreatmentRoomEventHandler>();
            services.AddScoped<INotificationHandler<TreatmentRoomRegisteredEvent>, TreatmentRoomEventHandler>();
            services.AddScoped<INotificationHandler<TreatmentRoomRemovedEvent>, TreatmentRoomEventHandler>();
            services.AddScoped<INotificationHandler<TreatmentRoomReservedEvent>, TreatmentRoomEventHandler>();
            
            services.AddScoped<IRequestHandler<RegisterConsultationCommand, bool>, ConsultationCommandHandler>();
         
            services.AddScoped<IRequestHandler<RegisterDoctorCommand, bool>, DoctorCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveDoctorCommand, bool>, DoctorCommandHandler>();
            services.AddScoped<IRequestHandler<ReserveDoctorCommand, bool>, DoctorCommandHandler>();
            
            services.AddScoped<IRequestHandler<RegisterCovidPatientCommand, bool>, PatientCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterFluPatientCommand, bool>, PatientCommandHandler>();
            services.AddScoped<IRequestHandler<RemovePatientCommand, bool>, PatientCommandHandler>();
            
            services.AddScoped<IRequestHandler<RegisterSimpleTreatmentMachineCommand, bool>, TreatmentMachineCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterAdvancedTreatmentMachineCommand, bool>, TreatmentMachineCommandHandler>();
            
            services.AddScoped<IRequestHandler<EquipTreatmentRoomWithMachineCommand, bool>, TreatmentRoomCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterTreatmentRoomCommand, bool>, TreatmentRoomCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveTreatmentRoomCommand, bool>, TreatmentRoomCommandHandler>();
            services.AddScoped<IRequestHandler<ReserveTreatmentRoomCommand, bool>, TreatmentRoomCommandHandler>();

            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IMailService, MailService>();

            services.AddScoped<IConsultationRepository, ConsultationRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<ITreatmentRoomRepository, TreatmentRoomRepository>();
            services.AddScoped<ITreatmentMachineRepository, TreatmentMachineRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
        }
    }
}