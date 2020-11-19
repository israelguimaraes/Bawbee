using System;

namespace Bawbee.Core.Events
{
    public interface IEvent
    {
        DateTime Timestamp { get; }
    }
}
