using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Queries.Users.DTOs;
using System.Collections.Generic;

namespace Bawbee.Domain.Queries.Users.Queries
{
    public class GetAllUsersQuery : CommandQuery<IEnumerable<UserDTO>>
    {

    }
}
