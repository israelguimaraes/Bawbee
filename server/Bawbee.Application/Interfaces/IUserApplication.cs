using Bawbee.Application.ViewModels.Users;
using System;
using System.Threading.Tasks;

namespace Bawbee.Application.Interfaces
{
    public interface IUserApplication : IDisposable
    {
        Task Register(RegisterUserViewModel viewModel);
    }
}
