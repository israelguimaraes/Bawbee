using Bawbee.SharedKernel.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.Bus
{
    public interface ICommandBus
    {
        Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default);
        Task PublishEvent<T>(T @event) where T : BaseEvent;
    }
}
