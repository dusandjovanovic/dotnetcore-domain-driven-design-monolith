using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DDDMedical.Application.EventSourcedNormalizers.Consultation;
using DDDMedical.Application.Interfaces;
using DDDMedical.Application.ViewModels;
using DDDMedical.Domain.Commands.Consultation;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Specifications;
using DDDMedical.Infrastructure.Data.Repository.EventSourcing;

namespace DDDMedical.Application.Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly IMapper _mapper;
        private readonly IConsultationRepository _consultationRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public ConsultationService(IMapper mapper, IConsultationRepository consultationRepository, 
            IMediatorHandler mediator, IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _consultationRepository = consultationRepository;
            _eventStoreRepository = eventStoreRepository;
            _mediator = mediator;
        }
        
        public void Register(ConsultationViewModel consultationViewModel)
        {
            var registerConsultationCommand = _mapper.Map<RegisterConsultationCommand>(consultationViewModel);
            _mediator.SendCommand(registerConsultationCommand);
        }

        public IEnumerable<ConsultationViewModel> GetAll()
        {
            return _consultationRepository.GetAll().ProjectTo<ConsultationViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<ConsultationViewModel> GetAll(int skip, int take)
        {
            return _consultationRepository.GetAll(new ConsultationFilterPaginatedSpecification(skip, take))
                .ProjectTo<ConsultationViewModel>(_mapper.ConfigurationProvider);
        }

        public ConsultationViewModel GetById(Guid id)
        {
            return _mapper.Map<ConsultationViewModel>(_consultationRepository.GetById(id));
        }

        public IList<ConsultationHistoryData> GetAllHistory(Guid id)
        {
            return ConsultationHistory.ToJavaScriptHistory(_eventStoreRepository.All(id));
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}