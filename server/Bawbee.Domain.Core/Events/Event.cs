using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using MediatR;
using System;

namespace Bawbee.Domain.Core.Events
{
    public abstract class Event : Message, INotification
    {
        public Guid Id { get; private set; }
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }

        public bool MustBeStored()
        {
            return MessageType != nameof(DomainNotification);
        }
    }
}
