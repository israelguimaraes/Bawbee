using Bawbee.Core.Aggregates.Entries.Shared;
using System.Threading.Tasks;

namespace Bawbee.Core.Aggregates.Entries
{
    public interface IEntryRepository
    {
        Task<BaseEntry> GetById(int id);
        Task Add(BaseEntry entry);
        Task Update(BaseEntry entry);
        Task Delete(int id);
    }
}
