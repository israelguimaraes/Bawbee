using Bawbee.Application.QueryStack.Users.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Application.QueryStack.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserReadModel>>
    {

    }
}
