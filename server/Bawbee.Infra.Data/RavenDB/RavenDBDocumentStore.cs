using Raven.Client.Documents;

namespace Bawbee.Infra.Data.RavenDB
{
    public class RavenDBDocumentStore : IDocumentStoreHolder
    {
        public IDocumentStore Store { get; }

        public RavenDBDocumentStore(RavenDBConfig ravenConfig)
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
