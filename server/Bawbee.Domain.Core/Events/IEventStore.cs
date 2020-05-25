namespace Bawbee.Domain.Core.Events
{
    public interface IEventStore
    {
        void Store<T>(T @event) where T : Event;
    }
}
