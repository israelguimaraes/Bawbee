namespace Bawbee.Domain.Core.Bus
{
    public interface IEventBusConnection<T>
    {
        void TryConnectIfNecessary();
        bool IsConnected();
        T CreateChannel();
    }
}
