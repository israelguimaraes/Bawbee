using Bawbee.Application.Interfaces;
using Bawbee.Application.Services;
using Bawbee.Domain.CommandHandlers;
using Bawbee.Domain.Commands.Users;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Domain.EventHandlers;
using Bawbee.Domain.Events.Users;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.CrossCutting.Bus;
using Bawbee.Infra.Data.EntityFramework.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bawbee.Infra.CrossCutting.IoC
{
    public class InjectorBootstrapper
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            // Domain
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<UserRegisteredEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<UserRegisteredEvent>, EmailEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewUserCommand, bool>, UserCommandHandler>();

            // Infra.Data
            services.AddScoped<IUserRepository, UserRepository>();

            // Application
            services.AddScoped<IUserApplication, UserApplication>();
        }
    }
}
