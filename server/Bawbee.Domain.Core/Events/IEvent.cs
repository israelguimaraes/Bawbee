using System;

namespace Bawbee.Domain.Core.Events
{
    public interface IEvent
    {
        DateTime Timestamp { get; }
    }
}
