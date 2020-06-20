using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Notifications;

namespace Bawbee.Domain.Core.Commands
{
    public abstract class BaseCommandHandler
    {
        private readonly IEventBus _eventBus;

        public BaseCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        protected void AddError(DomainNotification message)
        {
            _eventBus.Publish(new DomainNotification(message.Value));
        }
    }
}
