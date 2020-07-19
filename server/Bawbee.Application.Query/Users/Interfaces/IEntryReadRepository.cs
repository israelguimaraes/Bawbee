using Bawbee.Application.Query.Users.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Application.Query.Users.Interfaces
{
    public interface IEntryReadRepository
    {
        Task<IEnumerable<EntryDocument>> GetAllByUser(int userId);
    }
}
