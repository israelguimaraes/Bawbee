using System.Threading.Tasks;

namespace Bawbee.Domain.AggregatesModel.Entries
{
    public interface IEntryRepository
    {
        Task<Entry> GetById(int id);
        Task Add(Entry entry);
        Task Update(Entry entry);
        Task Delete(int id);
    }
}
