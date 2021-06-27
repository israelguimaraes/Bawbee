using MediatR;
using System;

namespace Bawbee.SharedKernel.Events
{
    public abstract class BaseEvent : INotification
    {
        public DateTime Timestamp { get; } = DateTime.Now;
    }
}
