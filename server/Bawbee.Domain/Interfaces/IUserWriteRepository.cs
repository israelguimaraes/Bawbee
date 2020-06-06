using Bawbee.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Domain.Interfaces
{
    public interface IUserWriteRepository
    {
        Task Add(User user);
    }
}
