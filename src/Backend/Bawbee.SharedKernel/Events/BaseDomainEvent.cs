using MediatR;
using System;

namespace Bawbee.SharedKernel.Events
{
    public class BaseDomainEvent : INotification
    {
        public DateTime Timestamp { get; } = DateTime.Now;
    }
}
