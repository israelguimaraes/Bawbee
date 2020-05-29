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
using Bawbee.Infra.Data.EntityFramework.Contexts;
using Bawbee.Infra.Data.EntityFramework.Repositories;
using Bawbee.Infra.Data.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bawbee.Infra.CrossCutting.IoC
{
    public class BawbeeInjectorBootstrapper
    {
        public static void RegisterDependencies(IServiceCollection services)
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<BawbeeDbContext>();

            // Application
            services.AddScoped<IUserApplication, UserApplication>();
        }
    }
}
