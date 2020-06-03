using Bawbee.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bawbee.API.Setups
{
    public static class DependencyInjectionSetup
    {
        public static void AddAllBawbeeDependencies(this IServiceCollection services)
        {
            BawbeeInjectorBootstrapper.RegisterDependencies(services);
        }
    }
}
