namespace Bawbee.Core.Events
{
    public class StoredEvent
    {
        public IEvent Data { get; private set; }

        public StoredEvent(IEvent eventObj)
        {
            Data = eventObj;
        }
    }
}
