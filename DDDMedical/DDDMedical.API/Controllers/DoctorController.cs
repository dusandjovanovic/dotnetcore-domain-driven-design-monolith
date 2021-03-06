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
    [Authorize]
    public class DoctorController : ApiController
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(
            IDoctorService doctorService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("doctor-management")]
        public IActionResult Get()
        {
            return Response(_doctorService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("doctor-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var customerViewModel = _doctorService.GetById(id);

            return Response(customerViewModel);
        }
        
        [HttpPost]
        [Authorize(Policy = "CanWriteDoctorData", Roles = Roles.Admin)]
        [Route("doctor-management")]
        public IActionResult Post([FromBody]DoctorViewModel doctorViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(doctorViewModel);
            }

            _doctorService.Register(doctorViewModel);

            return Response(doctorViewModel);
        }
        
        [HttpPut]
        [Authorize(Policy = "CanWriteDoctorData", Roles = Roles.Admin)]
        [Route("doctor-management")]
        public IActionResult Put([FromBody]DoctorViewModel doctorViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(doctorViewModel);
            }

            _doctorService.Reserve(doctorViewModel);

            return Response(doctorViewModel);
        }

        [HttpDelete]
        [Authorize(Policy = "CanRemoveDoctorData", Roles = Roles.Admin)]
        [Route("doctor-management")]
        public IActionResult Delete(Guid id)
        {
            _doctorService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("doctor-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var customerHistoryData = _doctorService.GetAllHistory(id);
            return Response(customerHistoryData);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("doctor-management/pagination")]
        public IActionResult Pagination(int skip, int take)
        {
            return Response(_doctorService.GetAll(skip, take));
        }
    }
}