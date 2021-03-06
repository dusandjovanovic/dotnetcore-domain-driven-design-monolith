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
    public class PatientController : ApiController
    {
        private readonly IPatientService _patientService;

        public PatientController(
            IPatientService patientService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("patient-management")]
        public IActionResult Get()
        {
            return Response(_patientService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("patient-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var customerViewModel = _patientService.GetById(id);

            return Response(customerViewModel);
        }
        
        [HttpPost]
        [Authorize(Policy = "CanWritePatientData", Roles = Roles.Admin)]
        [Route("patient-management/covid")]
        public IActionResult PostCovid([FromBody]PatientViewModel patientViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(patientViewModel);
            }

            _patientService.RegisterCovid(patientViewModel);

            return Response(patientViewModel);
        }
        
        [HttpPost]
        [Authorize(Policy = "CanWritePatientData", Roles = Roles.Admin)]
        [Route("patient-management/influenza")]
        public IActionResult PostInfluenza([FromBody]PatientViewModel patientViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(patientViewModel);
            }

            _patientService.RegisterFlu(patientViewModel);

            return Response(patientViewModel);
        }

        [HttpDelete]
        [Authorize(Policy = "CanRemovePatientData", Roles = Roles.Admin)]
        [Route("patient-management")]
        public IActionResult Delete(Guid id)
        {
            _patientService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("patient-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var customerHistoryData = _patientService.GetAllHistory(id);
            return Response(customerHistoryData);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("patient-management/pagination")]
        public IActionResult Pagination(int skip, int take)
        {
            return Response(_patientService.GetAll(skip, take));
        }
    }
}