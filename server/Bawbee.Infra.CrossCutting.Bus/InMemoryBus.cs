using Bawbee.Core.Bus;
using Bawbee.Core.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Infra.CrossCutting.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;
        private readonly IEventBus _bus;

        public InMemoryBus(IMediator mediator, IEventStore eventStore, IEventBus bus)
        {
            _mediator = mediator;
            _eventStore = eventStore;
            _bus = bus;
        }

        public Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            if (@event.IsDomainNotification())
            {
                await _mediator.Publish(@event);
                return;
            }

            if (@event.MustBeStored())
                await _eventStore.Store(@event);

            if (@event.MustBeSentToQueue())
                await _bus.Publish(@event);
        }
    }
}
