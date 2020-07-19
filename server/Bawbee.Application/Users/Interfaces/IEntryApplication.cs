using Bawbee.Application.Users.InputModels.Entries;
using Bawbee.Domain.Core.Commands;
using System.Threading.Tasks;

namespace Bawbee.Application.Users.Interfaces
{
    public interface IEntryApplication
    {
        Task<CommandResult> AddEntry(NewEntryInputModel model, int userId);
        Task<CommandResult> Update(UpdateEntryInputModel model, int userId);
        Task<CommandResult> GetAllByUser(int userId);
        Task<CommandResult> Delete(int entryId, int userId);
    }
}
