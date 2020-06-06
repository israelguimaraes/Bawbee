using Bawbee.Application.Interfaces;
using Bawbee.Application.Services;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Events;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.CrossCutting.Bus;
using Bawbee.Infra.Data.EventSource;
using Bawbee.Infra.Data.RavenDB;
using Bawbee.Infra.Data.ReadRepositories;
using Bawbee.Infra.Data.WriteRepositories;
using Bawbee.Infra.Data.WriteRepositories.Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bawbee.Infra.CrossCutting.IoC
{
    public class BawbeeInjectorBootstrapper
    {
        public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            // Domain
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            
            // Application
            services.AddScoped<IUserApplication, UserApplication>();

            // *** Infra.Data ***

            // RavenDB
            var ravenConfig = new RavenDBConfig();
            configuration.GetSection(nameof(RavenDBConfig)).Bind(ravenConfig);
            
            var ravenDocumentStore = new RavenDocumentStore(ravenConfig);
            services.TryAddSingleton<IDocumentStoreHolder>(d => ravenDocumentStore);

            // Repositories
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IDapperConnection, DapperConnection>();

            // EventSource
            services.AddScoped<IEventStore, RavenEventStore>();
        }
    }
}
