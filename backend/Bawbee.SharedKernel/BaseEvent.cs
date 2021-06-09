using MediatR;
using System;

namespace Bawbee.SharedKernel
{
    public abstract class BaseEvent : INotification
    {
        public DateTime Timestamp { get; } = DateTime.Now;
    }
}
