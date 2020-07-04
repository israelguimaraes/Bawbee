using Bawbee.Domain.Entities;
using System.Threading.Tasks;

namespace Bawbee.Domain.Interfaces
{
    public interface IEntryRepository
    {
        Task<Entry> GetById(int id);
        Task Add(Entry entry);
        Task Update(Entry entry);
    }
}
