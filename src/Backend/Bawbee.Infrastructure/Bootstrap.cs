﻿using Bawbee.Application.Bus;
using Bawbee.Core;
using Bawbee.Core.Aggregates.Users;
using Bawbee.Infrastructure.Bus;
using Bawbee.Infrastructure.IoC;
using Bawbee.Infrastructure.Persistence;
using Bawbee.Infrastructure.Persistence.SqlServer.EFCore.Contexts;
using Bawbee.Infrastructure.Persistence.SqlServer.EFCore.Repositories.Users;
using Bawbee.Infrastructure.Security.Jwt;
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
            services.AddScoped<ICommandBus, InMemoryBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IUserRepository, UserSqlRepository>();

            services.AddSingleton<ISecurityTokenService, JwtService>();
        }
    }
}
