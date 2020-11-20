using Bawbee.Application.QueryStack.Users.Queries;
using Bawbee.Application.QueryStack.Users.ReadModels;
using Bawbee.Core.Commands;
using Bawbee.Infra.Data.ReadInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.QueryStack.Users.Handlers
{
    public class UserQueryHandler :
        ICommandQueryHandler<GetAllUsersQuery, IEnumerable<UserReadModel>>,
        ICommandQueryHandler<GetAllCategoriesByUserQuery, IEnumerable<EntryCategoryReadModel>>,
        ICommandQueryHandler<GetAllBankAccountsByUserQuery, IEnumerable<BankAccountReadModel>>
    {
        private readonly IUserReadRepository _userReadRepository;

        public UserQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<IEnumerable<UserReadModel>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await _userReadRepository.GetAll();

            var result = users.Select(u => new UserReadModel
            {
                UserId = u.Id,
                Email = u.Email,
                Name = u.LastName,
                LastName = u.LastName
            });

            return result;
        }

        public async Task<IEnumerable<EntryCategoryReadModel>> Handle(GetAllCategoriesByUserQuery query, CancellationToken cancellationToken)
        {
            var categoriesDocument = await _userReadRepository.GetCategoriesByUser(query.UserId);

            return categoriesDocument.Select(c => new EntryCategoryReadModel
            {
                Id = c.CategoryId,
                Name = c.Name
            });
        }

        public async Task<IEnumerable<BankAccountReadModel>> Handle(GetAllBankAccountsByUserQuery query, CancellationToken cancellationToken)
        {
            var bankaccountsDocument = await _userReadRepository.GetBankAccountsByUser(query.UserId);

            return bankaccountsDocument.Select(b => new BankAccountReadModel
            {
                Id = b.BankAccountId,
                Name = b.Name,
                InitialBalance = b.InitialBalance
            });
        }
    }
}
