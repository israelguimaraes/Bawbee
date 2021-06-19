using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.SharedKernel.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public IEnumerable<DomainNotification> Notifications => _notifications;
        public bool HasNotifications => Notifications.Any();

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public void AddNotification(string notification)
        {
            _notifications.Add(new DomainNotification(notification));
        }
    }
}
