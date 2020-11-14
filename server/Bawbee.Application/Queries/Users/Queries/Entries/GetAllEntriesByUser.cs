using Bawbee.Application.Query.Users.ReadModels.Entries;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Application.Query.Users.Queries.Entries
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
