using Bawbee.Domain.Core.Events;
using System;

namespace Bawbee.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public Guid Id { get; private set; }
        public string Value { get; private set; }

        public DomainNotification(string value)
        {
            Value = value;
        }
    }
}
