using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DDDMedical.Application.EventSourcedNormalizers.Doctor;
using DDDMedical.Application.Interfaces;
using DDDMedical.Application.ViewModels;
using DDDMedical.Domain.Commands.Doctor;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Specifications;
using DDDMedical.Infrastructure.Data.Repository.EventSourcing;

namespace DDDMedical.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public DoctorService(IMapper mapper, IDoctorRepository doctorRepository, IMediatorHandler mediator,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _eventStoreRepository = eventStoreRepository;
            _mediator = mediator;
        }
        
        public void Register(DoctorViewModel doctorViewModel)
        {
            var registerDoctorCommand = _mapper.Map<RegisterDoctorCommand>(doctorViewModel);
            _mediator.SendCommand(registerDoctorCommand);
        }

        public IEnumerable<DoctorViewModel> GetAll()
        {
            return _doctorRepository.GetAll().ProjectTo<DoctorViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<DoctorViewModel> GetAll(int skip, int take)
        {
            return _doctorRepository.GetAll(new DoctorFilterPaginatedSpecification(skip, take))
                .ProjectTo<DoctorViewModel>(_mapper.ConfigurationProvider);
        }

        public DoctorViewModel GetById(Guid id)
        {
            return _mapper.Map<DoctorViewModel>(_doctorRepository.GetById(id));
        }

        public void Reserve(DoctorViewModel doctorViewModel)
        {
            var reserveDoctorCommand = _mapper.Map<ReserveDoctorCommand>(doctorViewModel);
            _mediator.SendCommand(reserveDoctorCommand);
        }

        public void Remove(Guid id)
        {
            var removeDoctorCommand = new RemoveDoctorCommand(id);
            _mediator.SendCommand(removeDoctorCommand);
        }

        public IList<DoctorHistoryData> GetAllHistory(Guid id)
        {
            return DoctorHistory.ToJavaScriptCustomerHistory(_eventStoreRepository.All(id));
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}