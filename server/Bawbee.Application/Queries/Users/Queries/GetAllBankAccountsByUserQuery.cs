using Bawbee.Application.Query.Users.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Application.Query.Users.Queries
{
    public class GetAllBankAccountsByUserQuery : IRequest<IEnumerable<BankAccountReadModel>>
    {
        public int UserId { get; }

        public GetAllBankAccountsByUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}
