using Bawbee.Application.Interfaces;
using Bawbee.Application.Services;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Events;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.CrossCutting.Bus;
using Bawbee.Infra.Data.DapperContext;
using Bawbee.Infra.Data.DapperContext.Interfaces;
using Bawbee.Infra.Data.DapperContext.Repositories;
using Bawbee.Infra.Data.EventSource;
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

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Infra.Data
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDapperConnection, DapperConnection>();

            // Infra.Data - EventSource
            services.AddScoped<IEventStore, RavenEventStore>();

            // Application
            services.AddScoped<IUserApplication, UserApplication>();
        }
    }
}
