using Bawbee.Domain.Commands.Users.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Domain.Commands.Users.Handlers
{
    public class UserEventHandler
        : INotificationHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent userRegistered, CancellationToken cancellationToken)
        {
            // TODO: send e-mail (validation user)
            return Task.CompletedTask;
        }
    }
}
