using AutoMapper;
using DDDMedical.Application.ViewModels;
using DDDMedical.Domain.Models;

namespace DDDMedical.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Consultation, ConsultationViewModel>();
            CreateMap<Doctor, DoctorViewModel>();
            CreateMap<Patient, PatientViewModel>();
            CreateMap<TreatmentRoom, TreatmentRoomViewModel>();
            CreateMap<TreatmentMachine, TreatmentMachineViewModel>();
        }
    }
}