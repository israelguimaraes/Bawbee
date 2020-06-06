namespace Bawbee.Domain.Core.Events
{
    public class StoredEvent
    {
        public Event Data { get; private set; }

        public StoredEvent(Event eventObj)
        {
            Data = eventObj;
        }
    }
}
