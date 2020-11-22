using Bawbee.Application.CommandStack.Admin.Commands;
using Bawbee.Core.Bus;
using Bawbee.Core.Commands;
using Bawbee.Core.Notifications;
using Bawbee.Core.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.CommandStack.Admin.Handlers
{
    public class AdminCommandHandler : CommandHandler,
        ICommandHandler<RecreateDatabaseAndSetInitialDataCommand>
    {
        public AdminCommandHandler(
            IMediatorHandler mediator, IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notificationHandler) 
            : base(mediator, unitOfWork, notificationHandler)
        {
        }

        public async Task<CommandResult> Handle(RecreateDatabaseAndSetInitialDataCommand command, CancellationToken cancellationToken)
        {
            // SQL - delete database


            // SQL - create database and tables


            // SQL - insert initial data
            return null;
        }
    }
}
