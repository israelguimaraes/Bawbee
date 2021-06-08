using MediatR;
using System;

namespace Bawbee.SharedKernel
{
    public class BaseDomainEvent : INotification
    {
        public DateTime Timestamp { get; } = DateTime.Now;
    }
}
