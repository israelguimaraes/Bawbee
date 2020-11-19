using Bawbee.Domain.AggregatesModel.Entries;
using System.Threading.Tasks;

namespace Bawbee.Domain.Interfaces
{
    public interface IEntryRepository
    {
        Task<Entry> GetById(int id);
        Task Add(Entry entry);
        Task Update(Entry entry);
        Task Delete(int id);
    }
}
