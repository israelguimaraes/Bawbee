using Bawbee.Application.InputModels.Users;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Queries.Users.ReadModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Application.Interfaces
{
    public interface IUserApplication : IDisposable
    {
        Task Register(RegisterNewUserInputModel model);
        //Task<CommandResult> Register(RegisterNewUserInputModel model);
        Task<IEnumerable<UserReadModel>> GetAll();
        //Task<UserLoginReadModel> Login(LoginInputModel model);
    }
}
