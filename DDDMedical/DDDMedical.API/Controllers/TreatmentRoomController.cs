using System;
using DDDMedical.Application.Interfaces;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Infrastructure.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDMedical.API.Controllers
{
    [Authorize]
    public class TreatmentRoomController : ApiController
    {
        private readonly ITreatmentRoomService _treatmentRoomService;

        public TreatmentRoomController(
            ITreatmentRoomService treatmentRoomService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _treatmentRoomService = treatmentRoomService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("room-management")]
        public IActionResult Get()
        {
            return Response(_treatmentRoomService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("room-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var customerViewModel = _treatmentRoomService.GetById(id);

            return Response(customerViewModel);
        }

        [HttpDelete]
        [Authorize(Policy = "CanRemoveTreatmentRoomData", Roles = Roles.Admin)]
        [Route("room-management")]
        public IActionResult Delete(Guid id)
        {
            _treatmentRoomService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("room-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var customerHistoryData = _treatmentRoomService.GetAllHistory(id);
            return Response(customerHistoryData);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("room-management/pagination")]
        public IActionResult Pagination(int skip, int take)
        {
            return Response(_treatmentRoomService.GetAll(skip, take));
        }
    }
}