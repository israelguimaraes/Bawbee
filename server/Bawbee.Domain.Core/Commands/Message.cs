namespace Bawbee.Domain.Core.Commands
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public string AggregateId { get; protected set; }

        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
