using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DDDMedical.API.Controllers
{
    public class RoleController : ApiController
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _roleManager = roleManager;
        }
    }
}