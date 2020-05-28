using MediatR;

namespace Bawbee.Domain.Core.Commands
{
    public abstract class Message : IRequest<bool>
    {
        public string MessageType { get; protected set; }
        public string AggregateId { get; protected set; }

        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
