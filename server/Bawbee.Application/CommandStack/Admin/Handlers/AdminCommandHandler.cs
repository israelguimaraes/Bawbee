using Bawbee.Application.CommandStack.Admin.Commands;
using Bawbee.Core.Bus;
using Bawbee.Core.Commands;
using Bawbee.Core.Notifications;
using Bawbee.Core.UnitOfWork;
using Bawbee.Infra.Data.ReadInterfaces;
using Bawbee.Infra.Data.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.CommandStack.Admin.Handlers
{
    public class AdminCommandHandler : CommandHandler,
        ICommandHandler<RecreateDatabaseAndSetInitialDataCommand>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAdminRavenDBRepository _adminRavenDBRepository;

        public AdminCommandHandler(
            IMediatorHandler mediator, 
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notificationHandler,
            IServiceProvider serviceProvider,
            IAdminRavenDBRepository adminRavenDBRepository) 
            : base(mediator, unitOfWork, notificationHandler)
        {
            _serviceProvider = serviceProvider;
            _adminRavenDBRepository = adminRavenDBRepository;
        }

        public async Task<CommandResult> Handle(RecreateDatabaseAndSetInitialDataCommand command, CancellationToken cancellationToken)
        {
            var time = Stopwatch.StartNew();

            using (var scope = _serviceProvider.CreateScope())
            {
                // RavenDB - delete all documents
                var ravenDB = _adminRavenDBRepository.DeleteAllDocuments();

                // SQL - delete database
                var sqlServer = Task.Run(() =>
                {
                    var context = scope.ServiceProvider.GetService<BawbeeDbContext>();
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
                });

                Task.WaitAll(ravenDB, sqlServer);

                // insert initial data
                await _adminRavenDBRepository.CreateInitialData();

                await CommitTransaction();

                time.Stop();

                var result = new
                {
                    TimeSpan.FromMilliseconds(time.ElapsedMilliseconds).TotalSeconds
                };

                return CommandResult.Ok(result);
            }
        }
    }
}
