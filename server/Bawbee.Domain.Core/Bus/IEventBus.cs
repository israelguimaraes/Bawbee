using Bawbee.Domain.Core.Events;
using System.Threading.Tasks;

namespace Bawbee.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task Publish(Event @event);
        void Subscribe<T>() where T : Event;
    }
}
