﻿using MediatR;

namespace Bawbee.Domain.Core.Commands
{
    public abstract class Message : IRequest<CommandResult>
    {
        public string MessageType { get; protected set; }
        public object AggregateId { get; protected set; }

        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
