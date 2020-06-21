namespace Bawbee.Domain.Core.Bus
{
    public interface IEventBusConnection<T>
    {
        void ConnectIfNecessary();
        bool IsConnected();
        T CreateChannel();
    }
}
