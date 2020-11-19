using Bawbee.Core.Bus;
using Bawbee.Core.Notifications;
using Bawbee.Core.UnitOfWork;
using MediatR;
using System.Threading.Tasks;

namespace Bawbee.Core.Commands
{
    public abstract class BaseCommandHandler
    {
        private readonly IMediatorHandler _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainNotificationHandler _notificationHandler;

        public BaseCommandHandler(IMediatorHandler mediator, IUnitOfWork unitOfWork, INotificationHandler<DomainNotification> notificationHandler)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _notificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        protected async Task AddDomainNotification(string message)
        {
            await _mediator.PublishEvent(new DomainNotification(message));
        }

        protected async Task<bool> CommitTransaction()
        {
            if (_notificationHandler.HasNotifications)
                return false;

            if (await _unitOfWork.CommitTransaction())
                return true;

            await _mediator.PublishEvent(new DomainNotification("Commit transaction failed."));
            return false;
        }
    }
}
