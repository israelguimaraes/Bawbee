using MediatR;

namespace Bawbee.Core.Commands
{
    public abstract class Message : IRequest<CommandResult>
    {
        public string MessageType { get; protected set; }

        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
