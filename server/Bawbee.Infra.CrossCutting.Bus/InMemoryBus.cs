using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Infra.CrossCutting.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IMediator mediator, IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }

        public Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }

        public async Task PublishEvent<T>(T eventObj) where T : Event
        {
            if (!eventObj.IsDomainNotification())
                await _eventStore.Store(eventObj);

            await _mediator.Publish(eventObj);
        }
    }
}
