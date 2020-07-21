using Raven.Client.Documents;
using Newtonsoft.Json;
using Raven.Client.Documents.Conventions;

namespace Bawbee.Infra.Data.RavenDB
{
    public class RavenDBDocumentStore : IDocumentStoreHolder
    {
        public IDocumentStore Store { get; }

        public RavenDBDocumentStore(RavenDBConfig ravenConfig)
        {
            Store = new DocumentStore
            {
                Urls = new[] { ravenConfig.ServerUrl },
                Database = ravenConfig.Database,
                Conventions = new DocumentConventions
                {
                    CustomizeJsonSerializer = serializer => serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            };

            Store.Initialize();
        }
    }
}
