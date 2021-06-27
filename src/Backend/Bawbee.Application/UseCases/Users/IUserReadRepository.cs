using Bawbee.Core.Aggregates.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Application.UseCases.Users
{
    public interface IUserReadRepository
    {
        Task<IEnumerable<Category>> GetCategories(int userId);
    }
}
