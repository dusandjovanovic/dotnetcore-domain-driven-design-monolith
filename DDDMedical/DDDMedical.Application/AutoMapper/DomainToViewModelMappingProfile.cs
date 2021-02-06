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
        }
    }
}