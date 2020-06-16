using Bawbee.Domain.Core.Interfaces;
using Raven.Client.Documents.Session;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace Bawbee.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IAsyncDocumentSession _ravenDBSession;
        private readonly TransactionScope _sqlTransaction;

        public UnitOfWork(IAsyncDocumentSession documentSession)
        {
            _ravenDBSession = documentSession;

            var options = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };
            _sqlTransaction = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
        }

        public async Task CommitTransaction()
        {
            try
            {
                await _ravenDBSession.SaveChangesAsync();

                _sqlTransaction.Complete();
                _sqlTransaction.Dispose();
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                throw;
            }
        }
    }
}
