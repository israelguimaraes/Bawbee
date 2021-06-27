using Bawbee.Application.Mediator;
using Bawbee.Infrastructure.Bus;
using Bawbee.Infrastructure.Configs;
using Bawbee.Infrastructure.Configs.Swagger;
using Bawbee.Infrastructure.Persistence.Sql.EfContexts;
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
            // EF
            services.AddDbContext<BawbeeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BawbeeDbConnection")));

            services.RegisterJwt(configuration);
            services.RegisterSwagger();

            services.AddScoped<BawbeeDbContext>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }
    }
}
