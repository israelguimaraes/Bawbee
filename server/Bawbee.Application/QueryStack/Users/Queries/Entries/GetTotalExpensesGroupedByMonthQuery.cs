using Bawbee.Application.QueryStack.Users.ReadModels.Entries;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Application.QueryStack.Users.Queries.Entries
{
    public class GetTotalExpensesGroupedByMonthQuery : IRequest<IEnumerable<MonthExpenseReadModel>>
    {
        public int UserId { get; }
        public int Month { get; }

        public GetTotalExpensesGroupedByMonthQuery(int month, int userId)
        {
            UserId = userId;
            Month = month;
        }
    }
}
