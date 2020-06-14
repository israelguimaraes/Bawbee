using Bawbee.Application.Users.InputModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Application.Users.Interfaces
{
    public interface IUserApplication : IDisposable
    {
        Task Register(RegisterNewUserInputModel model);
        //Task<CommandResult> Register(RegisterNewUserInputModel model);
        Task<IEnumerable<UserReadModel>> GetAll();
        //Task<UserLoginReadModel> Login(LoginInputModel model);
    }
}
