namespace Bawbee.Domain.Core.Events
{
    public interface IEventStore
    {
        void Store<T>(T eventObj) where T : Event;
    }
}
