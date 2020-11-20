using Bawbee.Core.Models;
using System.Threading.Tasks;

namespace Bawbee.Domain.AggregatesModel.Entries
{
    public interface IEntryRepository : IAggregateRepository<Entry>
    {
        Task<Expense> GetById(int id);
        Task Add(Entry entry);
        Task Update(Entry entry);
        Task Delete(int id);
    }
}
