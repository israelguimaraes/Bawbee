using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection.Abstractions;
using Bawbee.Domain.Events;
using Bawbee.Domain.Core.Bus;

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
