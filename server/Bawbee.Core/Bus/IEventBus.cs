using Bawbee.Core.Events;
using System.Threading.Tasks;

namespace Bawbee.Core.Bus
{
    public interface IEventBus
    {
        Task Publish(Event @event);
        void Subscribe<T>() where T : Event;
    }
}
