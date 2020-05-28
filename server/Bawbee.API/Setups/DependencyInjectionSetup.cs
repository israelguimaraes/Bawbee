using Bawbee.Application.Interfaces;
using Bawbee.Application.Services;
using Bawbee.Domain.Core.Bus;
using Bawbee.Infra.CrossCutting.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bawbee.API.Setups
{
    public class DependencyInjectionSetup
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IUserApplication, UserApplication>();
        }
    }
}
