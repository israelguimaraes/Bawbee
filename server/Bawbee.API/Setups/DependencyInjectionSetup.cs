using Bawbee.Infra.CrossCutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bawbee.API.Setups
{
    public static class DependencyInjectionSetup
    {
        public static void AddAllBawbeeDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            BawbeeInjectorBootstrapper.RegisterDependencies(services, configuration);
        }
    }
}
