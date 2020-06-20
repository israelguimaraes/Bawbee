using Bawbee.Application.Command.Users;
using Bawbee.Application.Query.Users.Interfaces;
using Bawbee.Application.Query.Users.Queries;
using Bawbee.Application.Services;
using Bawbee.Application.Users.Interfaces;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Events;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.CrossCutting.Bus;
using Bawbee.Infra.CrossCutting.Common.Security;
using Bawbee.Infra.Data.EventSource;
using Bawbee.Infra.Data.NoSQLRepositories;
using Bawbee.Infra.Data.RavenDB.EventHandlers;
using Bawbee.Infra.Data.SQLRepositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bawbee.Infra.CrossCutting.IoC
{
    public class BawbeeInjectorBootstrapper
    {
        public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            RegisterAssembliesForMediatr(services);

            services.RegisterJwt(configuration);

            // Domain
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Application
            services.AddScoped<IUserApplication, UserApplication>();

            // Infra.Data
            services.AddScoped<IUserRepository, UserSqlServerRepository>();
            services.AddScoped<IUserReadRepository, UserRavenDBRepository>();

            services.RegisterRavenDB(configuration);
            services.RegisterSqlServer(configuration);
            
            // Event Source
            services.AddScoped<IEventStore, RavenDBEventStore>();

            // Infra.CrossCutting.Common
            services.AddScoped<IJwtService, JwtService>();

            services.RegisterSwagger();
        }

        private static void RegisterAssembliesForMediatr(IServiceCollection services)
        {
            // Bawbee.Domain
            services.AddMediatR(typeof(BaseCommandHandler).GetTypeInfo().Assembly);

            // Bawbee.Domain.Core
            services.AddMediatR(typeof(BaseCommand).GetTypeInfo().Assembly);

            // Bawbee.Application
            services.AddMediatR(typeof(UserApplication).GetTypeInfo().Assembly);

            // Bawbee.Application.Command
            services.AddMediatR(typeof(LoginCommand).GetTypeInfo().Assembly);

            // Bawbee.Application.Query
            services.AddMediatR(typeof(GetAllUsersQuery).GetTypeInfo().Assembly);

            // Bawbee.Infra.Data
            services.AddMediatR(typeof(UserRavenDBHandler).GetTypeInfo().Assembly);
        }
    }
}
