using Bawbee.Application.Query.Users.Interfaces;
using Bawbee.Application.Query.Users.Queries;
using Bawbee.Application.Query.Users.ReadModels;
using Bawbee.Domain.Core.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.Query.Users.Handlers
{
    public class UserQueryHandler : 
        ICommandQueryHandler<GetAllUsersQuery, IEnumerable<UserReadModel>>,
        ICommandQueryHandler<GetAllCategoriesByUserQuery, IEnumerable<EntryCategoryReadModel>>
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
                Id = c.EntryCategoryId,
                Name = c.Name
            });
        }
    }
}
