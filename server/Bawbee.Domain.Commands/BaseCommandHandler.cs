﻿using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using MediatR;

namespace Bawbee.Domain.Commands
{
    public abstract class BaseCommandHandler
    {
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notificationHandler;

        public BaseCommandHandler(IMediatorHandler mediator, INotificationHandler<DomainNotification> notificationHandler)
        {
            _notificationHandler = (DomainNotificationHandler)notificationHandler;
            _mediator = mediator;
        }

        protected void SendNotificationsErrors(Command message)
        {
            // TODO: FluentValidation
            //foreach (var error in message.ValidationResult.Errors)
            //{
            //    _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            //}
        }
    }
}