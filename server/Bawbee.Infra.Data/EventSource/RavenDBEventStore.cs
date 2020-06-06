using Bawbee.Domain.Core.Events;

namespace Bawbee.Infra.Data.EventSource
{
    public class RavenDBEventStore : IEventStore
    {
        public RavenDBEventStore()
        {

        }

        public void Store<T>(T eventObj) where T : Event
        {
            // TODO: ...
        }
    }
}
