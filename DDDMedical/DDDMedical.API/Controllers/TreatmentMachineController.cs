using System;
using DDDMedical.Application.Interfaces;
using DDDMedical.Application.ViewModels;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Infrastructure.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDMedical.API.Controllers
{
    public class TreatmentMachineController : ApiController
    {
        private readonly ITreatmentMachineService _treatmentMachineService;

        public TreatmentMachineController(
            ITreatmentMachineService treatmentMachineService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _treatmentMachineService = treatmentMachineService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("machine-management")]
        public IActionResult Get()
        {
            return Response(_treatmentMachineService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("machine-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var customerViewModel = _treatmentMachineService.GetById(id);

            return Response(customerViewModel);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("machine-management/simple")]
        public IActionResult PostSimple([FromBody]TreatmentMachineViewModel treatmentMachineViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(treatmentMachineViewModel);
            }

            _treatmentMachineService.RegisterSimple(treatmentMachineViewModel);

            return Response(treatmentMachineViewModel);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("machine-management/advanced")]
        public IActionResult PostAdvanced([FromBody]TreatmentMachineViewModel treatmentMachineViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(treatmentMachineViewModel);
            }

            _treatmentMachineService.RegisterAdvanced(treatmentMachineViewModel);

            return Response(treatmentMachineViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("machine-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var customerHistoryData = _treatmentMachineService.GetAllHistory(id);
            return Response(customerHistoryData);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("machine-management/pagination")]
        public IActionResult Pagination(int skip, int take)
        {
            return Response(_treatmentMachineService.GetAll(skip, take));
        }
    }
}