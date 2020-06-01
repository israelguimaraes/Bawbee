using Bawbee.Domain.Core.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default);
        Task PublishEvent<T>(T eventObj) where T : Event;
    }
}
