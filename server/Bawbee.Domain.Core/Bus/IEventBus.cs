using Bawbee.Domain.Core.Events;

namespace Bawbee.Domain.Core.Bus
{
    public interface IEventBus
    {
        void Publish(Event @event);
    }
}
