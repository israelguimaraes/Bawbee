using Microsoft.AspNetCore.Builder;

namespace Bawbee.Infra.CrossCutting.IoC
{
    public static class DomainEventsToRabbitMQ
    {
        public static void RegisterDomainEventsToRabbitMQ(this IApplicationBuilder app)
        {
            //var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            //eventBus.Subscribe<UserRegisteredEvent>();
        }
    }
}
