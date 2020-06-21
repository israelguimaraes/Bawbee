using Bawbee.Domain.Core.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default);
        Task Publish(Event @event);
        void Subscribe<T>() where T : Event;
    }
}
