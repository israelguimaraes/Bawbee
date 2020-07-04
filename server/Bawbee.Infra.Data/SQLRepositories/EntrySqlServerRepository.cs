using Bawbee.Domain.Entities;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.Data.EF;
using Bawbee.Infra.Data.SQLRepositories.Dapper;
using Dapper;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.SQLRepositories
{
    public class EntrySqlServerRepository : IEntryRepository
    {
        private readonly IDapperConnection _dapper;
        private readonly BawbeeDbContext _dbContext;

        public EntrySqlServerRepository(IDapperConnection dapper, BawbeeDbContext dbContext)
        {
            _dapper = dapper;
            _dbContext = dbContext;
        }

        public async Task Add(Entry entry)
        {
            await _dbContext.Entries.AddAsync(entry);
        }

        public async Task<Entry> GetById(int id)
        {
            var query = $"SELECT * FROM Entries WHERE Id = {id}";

            return await _dapper.Connection.QueryFirstOrDefaultAsync<Entry>(query);
        }

        public Task Update(Entry entry)
        {
            _dbContext.Entries.Update(entry);

            return Task.CompletedTask;
        }
    }
}
