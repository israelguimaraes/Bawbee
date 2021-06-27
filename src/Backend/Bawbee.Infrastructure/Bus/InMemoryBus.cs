using Bawbee.Application.Mediator;
using Bawbee.SharedKernel.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Infrastructure.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }

        public Task PublishEvent<T>(T @event) where T : BaseEvent
        {
            return _mediator.Publish(@event);
        }
    }
}
