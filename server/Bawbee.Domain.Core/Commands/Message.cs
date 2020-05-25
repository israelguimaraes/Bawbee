namespace Bawbee.Domain.Core.Commands
{
    public abstract class Message<T>
    {
        public string MessageType { get; protected set; }
        public T AggregateId { get; protected set; }

        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
