using Bawbee.Domain.Queries.Users.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Domain.Queries.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserReadModel>>
    {

    }
}
