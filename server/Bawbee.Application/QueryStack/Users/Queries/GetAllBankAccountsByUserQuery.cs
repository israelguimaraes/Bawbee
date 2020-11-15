using Bawbee.Application.QueryStack.Users.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Application.QueryStack.Users.Queries
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
