using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Interfaces;
using Bawbee.Domain.Queries.Users.Queries;
using Bawbee.Domain.Queries.Users.ReadModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Domain.Queries.Users.Handlers
{
    public class UserQueryHandler
        : ICommandQueryHandler<GetAllUsersQuery, IEnumerable<UserReadModel>>
    {
        private readonly IUserRepository _userRepository;

        public UserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserReadModel>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll();

            var result = users.Select(u => new UserReadModel
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.LastName,
                LastName = u.LastName
            });

            return result;
        }
    }
}
