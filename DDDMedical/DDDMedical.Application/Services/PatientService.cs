using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DDDMedical.Application.EventSourcedNormalizers.Patient;
using DDDMedical.Application.Interfaces;
using DDDMedical.Application.ViewModels;
using DDDMedical.Domain.Commands.Patient;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Specifications;
using DDDMedical.Infrastructure.Data.Repository.EventSourcing;

namespace DDDMedical.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public PatientService(IMapper mapper, IPatientRepository patientRepository, IMediatorHandler mediator,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
            _eventStoreRepository = eventStoreRepository;
            _mediator = mediator;
        }
        
        public void RegisterCovid(PatientViewModel patientViewModel)
        {
            var registerPatientCommand = _mapper.Map<RegisterCovidPatientCommand>(patientViewModel);
            _mediator.SendCommand(registerPatientCommand);
        }

        public void RegisterFlu(PatientViewModel patientViewModel)
        {
            var registerPatientCommand = _mapper.Map<RegisterFluPatientCommand>(patientViewModel);
            _mediator.SendCommand(registerPatientCommand);
        }

        public IEnumerable<PatientViewModel> GetAll()
        {
            return _patientRepository.GetAll().ProjectTo<PatientViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<PatientViewModel> GetAll(int skip, int take)
        {
            return _patientRepository.GetAll(new PatientFilterPaginatedSpecification(skip, take))
                .ProjectTo<PatientViewModel>(_mapper.ConfigurationProvider);
        }

        public PatientViewModel GetById(Guid id)
        {
            return _mapper.Map<PatientViewModel>(_patientRepository.GetById(id));
        }

        public void Remove(Guid id)
        {
            var removePatientCommand = new RemovePatientCommand(id);
            _mediator.SendCommand(removePatientCommand);
        }

        public IList<PatientHistoryData> GetAllHistory(Guid id)
        {
            return PatientHistory.ToJavaScriptHistory(_eventStoreRepository.All(id));
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}