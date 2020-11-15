using Bawbee.Application.QueryStack.Users.ReadModels.Entries;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Application.QueryStack.Users.Queries.Entries
{
    public class GetAllEntriesByUser : IRequest<IEnumerable<EntryReadModel>>
    {
        public int UserId { get; }

        public GetAllEntriesByUser(int userId)
        {
            UserId = userId;
        }
    }
}
