using MediatR;
using System;

namespace Bawbee.SharedKernel.Events
{
    public abstract class BaseDomainEvent : INotification
    {
        public DateTime Timestamp { get; } = DateTime.Now;
    }
}
