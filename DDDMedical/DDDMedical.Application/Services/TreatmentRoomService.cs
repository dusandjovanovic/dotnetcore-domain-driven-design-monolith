using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DDDMedical.Application.EventSourcedNormalizers.TreatmentRoom;
using DDDMedical.Application.Interfaces;
using DDDMedical.Application.ViewModels;
using DDDMedical.Domain.Commands.TreatmentRoom;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Specifications;
using DDDMedical.Infrastructure.Data.Repository.EventSourcing;

namespace DDDMedical.Application.Services
{
    public class TreatmentRoomService : ITreatmentRoomService
    {
        private readonly IMapper _mapper;
        private readonly ITreatmentRoomRepository _treatmentRoomRepository;
        private readonly ITreatmentMachineRepository _treatmentMachineRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public TreatmentRoomService(IMapper mapper, ITreatmentRoomRepository treatmentRoomRepository,
            ITreatmentMachineRepository treatmentMachineRepository, IMediatorHandler mediator, 
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _treatmentRoomRepository = treatmentRoomRepository;
            _treatmentMachineRepository = treatmentMachineRepository;
            _eventStoreRepository = eventStoreRepository;
            _mediator = mediator;
        }
        
        public void Register(TreatmentRoomViewModel treatmentRoomViewModel)
        {
            var registerTreatmentRoomCommand = _mapper.Map<RegisterTreatmentRoomCommand>(treatmentRoomViewModel);
            _mediator.SendCommand(registerTreatmentRoomCommand);
        }

        public void Reserve(TreatmentRoomViewModel treatmentRoomViewModel)
        {
            var reserveTreatmentRoomCommand = _mapper.Map<ReserveTreatmentRoomCommand>(treatmentRoomViewModel);
            _mediator.SendCommand(reserveTreatmentRoomCommand);
        }

        public void Equip(TreatmentRoomViewModel treatmentRoomViewModel)
        {
            var equipTreatmentRoomCommand = _mapper.Map<EquipTreatmentRoomWithMachineCommand>(treatmentRoomViewModel);
            _mediator.SendCommand(equipTreatmentRoomCommand);
        }

        public IEnumerable<TreatmentRoomViewModel> GetAll()
        {
            return _treatmentRoomRepository.GetAll().ProjectTo<TreatmentRoomViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<TreatmentRoomViewModel> GetAll(int skip, int take)
        {
            return _treatmentRoomRepository.GetAll(new TreatmentRoomFilterPaginatedSpecification(skip, take))
                .ProjectTo<TreatmentRoomViewModel>(_mapper.ConfigurationProvider);
        }

        public TreatmentRoomViewModel GetById(Guid id)
        {
            return _mapper.Map<TreatmentRoomViewModel>(_treatmentRoomRepository.GetById(id));
        }

        public void Remove(Guid id)
        {
            var removeTreatmentRoomCommand = new RemoveTreatmentRoomCommand(id);
            _mediator.SendCommand(removeTreatmentRoomCommand);
        }

        public IList<TreatmentRoomHistoryData> GetAllHistory(Guid id)
        {
            return TreatmentRoomHistory.ToJavaScriptHistory(_eventStoreRepository.All(id));
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}