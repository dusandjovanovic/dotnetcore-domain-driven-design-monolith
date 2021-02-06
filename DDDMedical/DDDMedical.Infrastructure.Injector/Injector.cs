using DDDMedial.Infrastructure.Bus;
using DDDMedical.Application.Interfaces;
using DDDMedical.Application.Services;
using DDDMedical.Domain.CommandHandlers;
using DDDMedical.Domain.Commands;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Events;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.EventHandlers;
using DDDMedical.Domain.Events;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Services;
using DDDMedical.Infrastructure.Data.EventSourcing;
using DDDMedical.Infrastructure.Data.Repository;
using DDDMedical.Infrastructure.Data.Repository.EventSourcing;
using DDDMedical.Infrastructure.Data.UoW;
using DDDMedical.Infrastructure.Identity.Authorization;
using DDDMedical.Infrastructure.Identity.Models;
using DDDMedical.Infrastructure.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace DDDMedical.Infrastructure.IoC
{
    public class Injector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            services.AddScoped<ICustomerAppService, CustomerAppService>();

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, bool>, CustomerCommandHandler>();

            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IMailService, MailService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSmsMessageSender>();

            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
        }
    }
}