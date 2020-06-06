using Bawbee.Domain.Commands.Users.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Domain.Commands.Services.Handlers
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
