using Bawbee.Core.Bus;
using Bawbee.Core.Commands;
using Bawbee.Core.Events;
using Bawbee.Core.Notifications;
using Bawbee.Core.UnitOfWork;
using Bawbee.Domain.AggregatesModel.Users;
using Bawbee.Infra.CrossCutting.Bus;
using Bawbee.Infra.Data.RavenDB.Repositories;
using Bawbee.Infra.Data.SQLServer.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
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

            // Infra.Data
            services.AddScoped<IUserRepository, UserSqlServerRepository>();
            services.AddScoped<IUserReadRepository, UserRavenDBRepository>();

            services.AddScoped<IEntryRepository, EntrySqlServerRepository>();
            services.AddScoped<IEntryReadRepository, EntryRavenDBRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<BawbeeDbContext>();

            services.RegisterRavenDB(configuration);
            services.RegisterSqlServer(configuration);
            
            // Event Source
            services.AddScoped<IEventStore, RavenDBEventStore>();

            // Infra.CrossCutting.Common
            services.AddScoped<IJwtService, JwtService>();

            services.RegisterSwagger();

            services.AddSingleton<IEventBus, RabbitMQEventBus>();
            services.AddSingleton<IEventBusConnection<IModel>, RabbitMQConnection>();
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


        // TODO: remove from setup
        private static void RegisterEventsToRabbitMQ(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<UserRegisteredEvent>();
        }
    }
}
