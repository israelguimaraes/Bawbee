namespace Bawbee.SharedKernel.Notifications
{
    public class DomainNotification : BaseEvent
    {
        public string Message { get; private set; }

        public DomainNotification(string message)
        {
            Message = message;
        }
    }
}
