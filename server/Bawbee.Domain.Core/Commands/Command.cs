using System;
using System.Collections.Generic;
using System.Text;

namespace Bawbee.Domain.Core.Commands
{
    public abstract class Command<TAggregateId> : Message<TAggregateId>
    {
        public DateTime Timestamp { get; set; }
    }
}
