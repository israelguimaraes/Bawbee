using Bawbee.Application.QueryStack.Users.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Application.QueryStack.Users.Queries
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
