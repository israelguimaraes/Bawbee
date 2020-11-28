using Bawbee.Infra.Data.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.ReadInterfaces
{
    public interface IEntryReadRepository
    {
        Task<IEnumerable<EntryDocument>> GetMonthEntries(int userId, int month);
        Task<IEnumerable<EntryDocument>> GetAllExpensesByMonth(int userId, int month);
    }
}
