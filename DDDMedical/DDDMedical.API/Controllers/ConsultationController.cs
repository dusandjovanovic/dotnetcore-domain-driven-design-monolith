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
    public class ConsultationController : ApiController
    {
        private readonly IConsultationService _consultationService;

        public ConsultationController(
            IConsultationService consultationService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _consultationService = consultationService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("consultation-management")]
        public IActionResult Get()
        {
            return Response(_consultationService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("consultation-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var customerViewModel = _consultationService.GetById(id);

            return Response(customerViewModel);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("consultation-management")]
        public IActionResult Post([FromBody]ConsultationViewModel consultationViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(consultationViewModel);
            }

            _consultationService.Register(consultationViewModel);

            return Response(consultationViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("consultation-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var customerHistoryData = _consultationService.GetAllHistory(id);
            return Response(customerHistoryData);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("consultation-management/pagination")]
        public IActionResult Pagination(int skip, int take)
        {
            return Response(_consultationService.GetAll(skip, take));
        }
    }
}