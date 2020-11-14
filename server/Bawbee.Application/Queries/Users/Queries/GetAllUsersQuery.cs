using Bawbee.Application.Query.Users.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Application.Query.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserReadModel>>
    {

    }
}
