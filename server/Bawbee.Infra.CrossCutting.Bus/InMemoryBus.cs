using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Events;
using MediatR;
using System;
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

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task PublishEvent<T>(T eventObj) where T : Event
        {
            return _mediator.Publish(eventObj);
        }
    }
}
