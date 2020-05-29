using Bawbee.Domain.Core.Events;

namespace Bawbee.Infra.Data.EventSource
{
    public class RavenEventStore : IEventStore
    {
        public void Store<T>(T eventObj) where T : Event
        {
            // TODO: ...
        }
    }
}
