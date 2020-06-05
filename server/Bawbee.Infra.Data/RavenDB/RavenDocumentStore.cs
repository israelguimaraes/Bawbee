using Raven.Client.Documents;

namespace Bawbee.Infra.Data.RavenDB
{
    public class RavenDocumentStore : IDocumentStoreHolder
    {
        public IDocumentStore Store { get; }

        public RavenDocumentStore(RavenDBConfig ravenConfig)
        {
            Store = new DocumentStore
            {
                Urls = new[] { ravenConfig.Url },
                Database = ravenConfig.Database
            };

            Store.Initialize();
        }
    }
}
