using Bawbee.Core.Events;

namespace Bawbee.Core.Notifications
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
