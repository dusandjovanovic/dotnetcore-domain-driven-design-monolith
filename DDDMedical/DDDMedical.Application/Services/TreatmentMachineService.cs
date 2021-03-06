using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DDDMedical.Application.EventSourcedNormalizers.TreatmentMachine;
using DDDMedical.Application.Interfaces;
using DDDMedical.Application.ViewModels;
using DDDMedical.Domain.Commands.TreatmentMachine;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Specifications;
using DDDMedical.Infrastructure.Data.Repository.EventSourcing;

namespace DDDMedical.Application.Services
{
    public class TreatmentMachineService : ITreatmentMachineService
    {
        private readonly IMapper _mapper;
        private readonly ITreatmentMachineRepository _treatmentMachineRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public TreatmentMachineService(IMapper mapper, ITreatmentMachineRepository treatmentMachineRepository,
            IMediatorHandler mediator, IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _treatmentMachineRepository = treatmentMachineRepository;
            _eventStoreRepository = eventStoreRepository;
            _mediator = mediator;
        }
        public void RegisterSimple(TreatmentMachineViewModel treatmentMachineViewModel)
        {
            var registerTreatmentMachineCommand = _mapper.Map<RegisterSimpleTreatmentMachineCommand>(treatmentMachineViewModel);
            _mediator.SendCommand(registerTreatmentMachineCommand);
        }

        public void RegisterAdvanced(TreatmentMachineViewModel treatmentMachineViewModel)
        {
            var registerTreatmentMachineCommand = _mapper.Map<RegisterAdvancedTreatmentMachineCommand>(treatmentMachineViewModel);
            _mediator.SendCommand(registerTreatmentMachineCommand);
        }

        public IEnumerable<TreatmentMachineViewModel> GetAll()
        {
            return _treatmentMachineRepository.GetAll().ProjectTo<TreatmentMachineViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<TreatmentMachineViewModel> GetAll(int skip, int take)
        {
            return _treatmentMachineRepository.GetAll(new TreatmentMachineFilterPaginatedSpecification(skip, take))
                .ProjectTo<TreatmentMachineViewModel>(_mapper.ConfigurationProvider);
        }

        public TreatmentMachineViewModel GetById(Guid id)
        {
            return _mapper.Map<TreatmentMachineViewModel>(_treatmentMachineRepository.GetById(id));
        }

        public IList<TreatmentMachineHistoryData> GetAllHistory(Guid id)
        {
            return TreatmentMachineHistory.ToJavaScriptHistory(_eventStoreRepository.All(id));
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}