using MediatR;

namespace Bawbee.SharedKernel.Notifications
{
    public class DomainNotification : INotification
    {
        public string Message { get; private set; }

        public DomainNotification(string value)
        {
            Message = value;
        }
    }
}
