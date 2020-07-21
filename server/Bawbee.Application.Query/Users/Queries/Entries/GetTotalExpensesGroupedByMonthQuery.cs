using Bawbee.Application.Query.Users.ReadModels.Entries;
using MediatR;
using System.Collections.Generic;

namespace Bawbee.Application.Query.Users.Queries.Entries
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
