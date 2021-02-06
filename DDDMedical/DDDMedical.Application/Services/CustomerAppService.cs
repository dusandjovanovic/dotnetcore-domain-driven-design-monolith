using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DDDMedical.Application.EventSourcedNormalizers;
using DDDMedical.Application.Interfaces;
using DDDMedical.Application.ViewModels;
using DDDMedical.Domain.Commands;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Specifications;
using DDDMedical.Infrastructure.Data.Repository.EventSourcing;

namespace DDDMedical.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public CustomerAppService(IMapper mapper, ICustomerRepository customerRepository, IMediatorHandler mediator,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _eventStoreRepository = eventStoreRepository;
            _mediator = mediator;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            return _customerRepository.GetAll().ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<CustomerViewModel> GetAll(int skip, int take)
        {
            return _customerRepository.GetAll(new CustomerFilterPaginatedSpecification(skip, take))
                .ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);
        }
        
        public IList<CustomerHistoryData> GetAllHistory(Guid id)
        {
            return CustomerHistory.ToJavaScriptCustomerHistory(_eventStoreRepository.All(id));
        }

        public CustomerViewModel GetById(Guid id)
        {
            return _mapper.Map<CustomerViewModel>(_customerRepository.GetById(id));
        }
        
        public void Register(CustomerViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(customerViewModel);
            _mediator.SendCommand(registerCommand);
        }

        public void Update(CustomerViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
            _mediator.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCustomerCommand(id);
            _mediator.SendCommand(removeCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}