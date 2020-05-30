using Bawbee.Application.InputModels.Users;
using System;
using System.Threading.Tasks;

namespace Bawbee.Application.Interfaces
{
    public interface IUserApplication : IDisposable
    {
        Task Register(RegisterNewUserInputModel model);
    }
}
