using Bawbee.Application.Query.Users.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Application.Query.Users.Queries
{
    public class GetAllCategoriesByUserQuery : IRequest<IEnumerable<EntryCategoryReadModel>>
    {
        public int UserId { get; }

        public GetAllCategoriesByUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}
