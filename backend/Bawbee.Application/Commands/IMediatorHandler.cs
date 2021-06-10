using Bawbee.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.Commands
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default);
        Task PublishEvent<T>(T @event) where T : BaseEvent;
    }
}
