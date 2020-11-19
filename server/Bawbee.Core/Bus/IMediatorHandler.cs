using Bawbee.Core.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Core.Bus
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default);
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
