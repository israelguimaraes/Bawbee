using Bawbee.Application.Operations;
using Bawbee.Core;
using Bawbee.SharedKernel.Notifications;
using MediatR;
using System.Threading.Tasks;

namespace Bawbee.Application.Mediator
{
    public abstract class BaseCommandHandler
    {
        private readonly IMediatorHandler _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainNotificationHandler _notificationHandler;

        protected BaseCommandHandler(
            IMediatorHandler mediator, 
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notificationHandler)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _notificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        protected async Task AddNotification(string message)
        {
            await _mediator.PublishEvent(new DomainNotification(message));
        }

        protected async Task<bool> CommitTransaction()
        {
            if (_notificationHandler.HasNotifications)
                return false;

            if (await _unitOfWork.CommitTransaction())
                return true;

            await AddNotification("Commit transaction failed.");
            return false;
        }

        protected OperationResult Ok(object data = null)
        {
            return new OkOperation(data);
        }

        protected OperationResult Invalid(string message = null)
        {
            return new InvalidOperation(message);
        }
    }
}
