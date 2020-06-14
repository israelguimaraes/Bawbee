using Bawbee.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.Services.Handlers
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
