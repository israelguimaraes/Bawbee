using Bawbee.Domain.Events.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Domain.EventHandlers
{
    public class EmailEventHandler
        : INotificationHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            // TODO: send e-mail
            return Task.CompletedTask;
        }
    }
}
