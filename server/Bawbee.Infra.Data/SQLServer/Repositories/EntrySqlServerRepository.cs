using Bawbee.Domain.AggregatesModel.Entries;
using Bawbee.Infra.Data.SQLServer.Dapper;
using Dapper;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.SQLServer.Repositories
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

        public async Task Delete(int id)
        {
            var entry = await GetById(id);

            _dbContext.Entries.Remove(entry);
        }
    }
}
