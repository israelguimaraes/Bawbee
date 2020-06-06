using Bawbee.Infra.Data.RavenDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents.Session;

namespace Bawbee.Infra.CrossCutting.IoC
{
    public static class RavenDBInjector
    {
        // Info: https://ayende.com/blog/187906-B/using-ravendb-unit-of-work-and-net-core-mvc
        public static void RegisterRavenDB(this IServiceCollection services, IConfiguration configuration)
        {
            // DocumentStore - Singleton: https://ravendb.net/docs/article-page/4.2/csharp/start/getting-started
            var ravenDocumentStore = new RavenDBDocumentStore(configuration.GetSection(nameof(RavenDBConfig)).Get<RavenDBConfig>());
            services.AddSingleton<IDocumentStoreHolder>(ravenDocumentStore);

            // One Session per request
            services.AddScoped<IAsyncDocumentSession>(serviceProvider =>
            {
                return serviceProvider
                    .GetService<IDocumentStoreHolder>()
                    .Store
                    .OpenAsyncSession();
            });
        }
    }
}