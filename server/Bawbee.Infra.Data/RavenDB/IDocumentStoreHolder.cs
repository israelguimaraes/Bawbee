using Raven.Client.Documents;

namespace Bawbee.Infra.Data.RavenDB
{
    public interface IDocumentStoreHolder
    {
        IDocumentStore Store { get; }
    }
}
