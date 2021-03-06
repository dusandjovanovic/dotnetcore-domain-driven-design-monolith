using AutoMapper;
using DDDMedical.Application.ViewModels;
using DDDMedical.Domain.Commands;
using DDDMedical.Domain.Commands.Consultation;
using DDDMedical.Domain.Commands.Doctor;
using DDDMedical.Domain.Commands.Patient;
using DDDMedical.Domain.Commands.TreatmentMachine;
using DDDMedical.Domain.Commands.TreatmentRoom;

namespace DDDMedical.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ConsultationViewModel, RegisterConsultationCommand>()
                .ConstructUsing(c => new RegisterConsultationCommand(c.Id, c.PatientId, c.DoctorId, c.TreatmentRoomId,
                    c.RegistrationDate, c.ConsultationDate));
            
            CreateMap<DoctorViewModel, RegisterDoctorCommand>()
                .ConstructUsing(c => new RegisterDoctorCommand(c.Id, c.Name, c.Email, c.Roles, c.Reservations));
            CreateMap<DoctorViewModel, RemoveDoctorCommand>()
                .ConstructUsing(c => new RemoveDoctorCommand(c.Id));
            CreateMap<DoctorViewModel, ReserveDoctorCommand>()
                .ConstructUsing(c => new ReserveDoctorCommand(c.Id, c.ReservationDay, c.ReferenceId));

            CreateMap<PatientViewModel, RegisterCovidPatientCommand>()
                .ConstructUsing(c => new RegisterCovidPatientCommand(c.Id, c.Name, c.Email, c.RegistrationDate));
            CreateMap<PatientViewModel, RegisterFluPatientCommand>()
                .ConstructUsing(c => new RegisterFluPatientCommand(c.Id, c.Name, c.Email, c.RegistrationDate));
            CreateMap<PatientViewModel, RemovePatientCommand>()
                .ConstructUsing(c => new RemovePatientCommand(c.Id));
            
            CreateMap<TreatmentMachineViewModel, RegisterAdvancedTreatmentMachineCommand>()
                .ConstructUsing(c => new RegisterAdvancedTreatmentMachineCommand(c.Id, c.Name));
            CreateMap<TreatmentMachineViewModel, RegisterSimpleTreatmentMachineCommand>()
                .ConstructUsing(c => new RegisterSimpleTreatmentMachineCommand(c.Id, c.Name));
            
            CreateMap<TreatmentRoomViewModel, RegisterTreatmentRoomCommand>()
                .ConstructUsing(c => new RegisterTreatmentRoomCommand(c.Id, c.TreatmentMachineId, c.Name));
            CreateMap<TreatmentRoomViewModel, EquipTreatmentRoomWithMachineCommand>()
                .ConstructUsing(c => new EquipTreatmentRoomWithMachineCommand(c.Id, c.TreatmentMachineId));
            CreateMap<TreatmentRoomViewModel, ReserveTreatmentRoomCommand>()
                .ConstructUsing(c => new ReserveTreatmentRoomCommand(c.Id, c.ReservationDay, c.TreatmentMachineId));
            CreateMap<TreatmentRoomViewModel, RemoveTreatmentRoomCommand>()
                .ConstructUsing(c => new RemoveTreatmentRoomCommand(c.Id));
        }
    }
}