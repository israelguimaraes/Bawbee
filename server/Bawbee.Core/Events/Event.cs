using Bawbee.Core.Commands;
using Bawbee.Core.Notifications;
using MediatR;
using System;

namespace Bawbee.Core.Events
{
    public abstract class Event : Message, IEvent, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }

        public bool MustBeStored()
        {
            return !IsDomainNotification();
        }

        public bool MustBeSentToQueue()
        {
            return !IsDomainNotification();
        }

        public bool IsDomainNotification()
        {
            return MessageType == nameof(DomainNotification);
        }
    }
}
