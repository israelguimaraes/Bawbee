﻿using Bawbee.Application.Mediator;
using Bawbee.Core;
using Bawbee.Core.Aggregates.Users;
using Bawbee.Infrastructure.Bus;
using Bawbee.Infrastructure.Configs;
using Bawbee.Infrastructure.Persistence;
using Bawbee.Infrastructure.Persistence.Sql.EfContexts;
using Bawbee.Infrastructure.Persistence.Sql.Interfaces;
using Bawbee.Infrastructure.Persistence.Sql.Repositories.Users;
using Bawbee.SharedKernel.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bawbee.Infrastructure
{
    public static class Bootstrap
    {
        public static void InitializeDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(BaseCommand).Assembly);

            // EF
            services.AddDbContext<BawbeeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BawbeeDbConnection")));

            services.RegisterJwt(configuration);
            services.RegisterSwagger();
            services.RegisterDapper(configuration);

            services.AddScoped<BawbeeDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IUserRepository, UserSqlRepository>();
        }
    }
}
