using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;

namespace Bawbee.Application.Bases
{
    public abstract class BaseCommandHandler
    {
        private readonly IMediatorHandler _mediator;

        public BaseCommandHandler(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        protected void SendNotificationsErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
                _mediator.PublishEvent(new DomainNotification(error.ErrorMessage));
        }
    }
}
