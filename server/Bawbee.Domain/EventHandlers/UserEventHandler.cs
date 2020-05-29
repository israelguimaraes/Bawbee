using Bawbee.Domain.Events.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Domain.EventHandlers
{
    public class UserEventHandler 
        : INotificationHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            // TODO: send e-mail (validation user)
            return Task.CompletedTask;
        }
    }
}
