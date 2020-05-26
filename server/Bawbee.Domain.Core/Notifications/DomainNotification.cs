using Bawbee.Domain.Core.Events;

namespace Bawbee.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public string Value { get; private set; }

        public DomainNotification(string value)
        {
            Value = value;
        }
    }
}
