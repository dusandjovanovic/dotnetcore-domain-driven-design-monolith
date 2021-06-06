using System.Linq;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDMedical.API.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IUser _user;
        private readonly ILogger _logger;

        public AccountController(
            IUser user,
            ILoggerFactory loggerFactory,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _user = user;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        [Route("current")]
        public IActionResult GetCurrent()
        {
            return Response(new
            {
                IsAuthenticated = _user.IsAuthenticated(),
                ClaimsIdentity = _user.GetClaimsIdentity().Select(x => new { x.Type, x.Value }),
            });
        }
    }
}