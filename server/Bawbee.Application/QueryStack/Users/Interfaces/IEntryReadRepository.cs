using Bawbee.Application.QueryStack.Users.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Application.QueryStack.Users.Interfaces
{
    public interface IEntryReadRepository
    {
        Task<IEnumerable<EntryDocument>> GetAllByUser(int userId);
        Task<IEnumerable<EntryDocument>> GetAllExpensesByMonth(int userId, int month);
    }
}
