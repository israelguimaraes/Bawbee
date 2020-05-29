using Bawbee.Domain.Core.Bus;
using Bawbee.Infra.CrossCutting.Bus;
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
            //services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            //services.AddScoped<INotificationHandler<UserRegisteredEvent>, UserEventHandler>();
            //services.AddScoped<INotificationHandler<UserRegisteredEvent>, EmailEventHandler>();

            //// Domain - Commands
            //services.AddScoped<IRequestHandler<RegisterNewUserCommand, bool>, UserCommandHandler>();

            //// Infra.Data
            //services.AddScoped<UserRepository, IUserRepository>();

            //// Application
            //services.AddScoped<IUserApplication, UserApplication>();
        }
    }
}
