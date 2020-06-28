using Bawbee.Application.Users.InputModels.Entries;
using Bawbee.Domain.Core.Commands;
using System.Threading.Tasks;

namespace Bawbee.Application.Users.Interfaces
{
    public interface IEntryApplication
    {
        Task<CommandResult> AddNewEntry(NewEntryInputModel model);
    }
}
