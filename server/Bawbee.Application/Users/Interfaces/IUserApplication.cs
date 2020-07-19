using Bawbee.Application.Query.Users.ReadModels;
using Bawbee.Application.Users.InputModels;
using Bawbee.Application.Users.InputModels.BankAccounts;
using Bawbee.Application.Users.InputModels.Categories;
using Bawbee.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Application.Users.Interfaces
{
    public interface IUserApplication : IDisposable
    {
        Task<CommandResult> Register(RegisterNewUserInputModel model);
        Task<IEnumerable<UserReadModel>> GetAll();
        Task<CommandResult> Login(LoginInputModel model);
        Task<IEnumerable<EntryCategoryReadModel>> GetCategories(int userId);
        Task<IEnumerable<BankAccountReadModel>> GetBankAccounts(int userId);
        Task<CommandResult> AddCategory(AddEntryCategoryInputModel model, int userId);
        Task<CommandResult> AddBankAccount(AddBankAccountInputModel model, int userId);
    }
}
