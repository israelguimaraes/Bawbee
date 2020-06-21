using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using MediatR;
using System;

namespace Bawbee.Domain.Core.Events
{
    public abstract class Event : Message, IEvent, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }

        public bool IsDomainNotification()
        {
            return MessageType == nameof(DomainNotification);
        }
    }
}
