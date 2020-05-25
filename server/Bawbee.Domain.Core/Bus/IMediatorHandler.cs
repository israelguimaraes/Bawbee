using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Events;
using System.Threading.Tasks;

namespace Bawbee.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
