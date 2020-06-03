using Bawbee.Application.Interfaces;
using Bawbee.Application.Services;
using Bawbee.Domain.CommandHandlers;
using Bawbee.Domain.Commands.Users;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Events;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Domain.EventHandlers;
using Bawbee.Domain.Events.Users;
using Bawbee.Domain.Interfaces;
using Bawbee.Domain.Queries.Users.Handlers;
using Bawbee.Domain.Queries.Users.Queries;
using Bawbee.Domain.Queries.Users.ReadModels;
using Bawbee.Infra.CrossCutting.Bus;
using Bawbee.Infra.Data.DapperContext.Interfaces;
using Bawbee.Infra.Data.DapperContext.Repositories;
using Bawbee.Infra.Data.EventSource;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Bawbee.Infra.CrossCutting.IoC
{
    public class BawbeeInjectorBootstrapper
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            // Domain
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Infra.Data
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDapperConnection, Bawbee.Infra.Data.DapperContext.DapperConnection>();
            //services.AddDbContext<BawbeeDbContext>();

            // Infra.Data - EventSource
            services.AddScoped<IEventStore, RavenEventStore>();

            // Application
            services.AddScoped<IUserApplication, UserApplication>();
        }
    }
}
