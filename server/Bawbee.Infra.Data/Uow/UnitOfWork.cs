﻿using Bawbee.Core.UnitOfWork;
using Bawbee.Infra.Data.SQLServer;
using System;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BawbeeDbContext _dbContext;

        public UnitOfWork(BawbeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CommitTransaction()
        {
            try
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // TODO: log
                return false;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
