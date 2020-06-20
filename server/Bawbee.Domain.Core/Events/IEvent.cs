using System;
using System.Collections.Generic;
using System.Text;

namespace Bawbee.Domain.Core.Events
{
    public interface IEvent
    {
        //Guid Id { get; }
        DateTime Timestamp { get; }

        //bool MustBeStored();
    }
}
