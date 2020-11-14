using Bawbee.Application.Query.Users.Interfaces;
using Bawbee.Application.Query.Users.Queries;
using Bawbee.Application.Query.Users.Queries.Entries;
using Bawbee.Application.Query.Users.ReadModels;
using Bawbee.Application.Query.Users.ReadModels.Entries;
using Bawbee.Domain.Core.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.Query.Users.Handlers
{
    public class EntryQueryHandler :
        ICommandQueryHandler<GetAllEntriesByUser, IEnumerable<EntryReadModel>>,
        ICommandQueryHandler<GetTotalExpensesGroupedByMonthQuery, IEnumerable<MonthExpenseReadModel>>
    {
        private readonly IEntryReadRepository _entryReadRepository;

        public EntryQueryHandler(IEntryReadRepository entryReadRepository)
        {
            _entryReadRepository = entryReadRepository;
        }

        public async Task<IEnumerable<EntryReadModel>> Handle(GetAllEntriesByUser query, CancellationToken cancellationToken)
        {
            var entries = await _entryReadRepository.GetAllByUser(query.UserId);

            return entries.Select(e => new EntryReadModel
            {
                Id = e.EntryId,
                Description = e.Description,
                Value = e.Value,
                CategoryName = e.EntryCategoryName,
                BankAccountName = e.BankAccountName,
                IsPaid = e.IsPaid,
                CreatedAt = e.CreatedAt
            });
        }

        public async Task<IEnumerable<MonthExpenseReadModel>> Handle(GetTotalExpensesGroupedByMonthQuery query, CancellationToken cancellationToken)
        {
            var expenses = await _entryReadRepository.GetAllExpensesByMonth(query.UserId, query.Month);

            var groupedByCategory = expenses.GroupBy(e => e.EntryCategoryId);

            var result = new List<MonthExpenseReadModel>();
            var totalExpenses = expenses.Sum(e => e.Value).ToPositive();

            foreach (var expensesByCategory in groupedByCategory)
            {
                var readModel = new MonthExpenseReadModel();
                readModel.Category = expenses.FirstOrDefault(e => e.EntryCategoryId == expensesByCategory.Key).EntryCategoryName;
                
                readModel.TotalValue = expensesByCategory.Sum(e => e.Value).ToPositive();

                readModel.Percent = (readModel.TotalValue / totalExpenses * 100).To2DecimalPlaces();

                result.Add(readModel);
            }

            return result;
        }
    }
}
