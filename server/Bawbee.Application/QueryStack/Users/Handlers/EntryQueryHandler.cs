﻿using Bawbee.Application.QueryStack.Users.Queries.Entries;
using Bawbee.Application.QueryStack.Users.ReadModels.Entries;
using Bawbee.Core.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using Bawbee.Infra.Data.ReadInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.QueryStack.Users.Handlers
{
    public class EntryQueryHandler :
        ICommandQueryHandler<GetMonthEntriesQuery, IEnumerable<EntryReadModel>>,
        ICommandQueryHandler<GetTotalExpensesGroupedByMonthQuery, IEnumerable<MonthExpenseReadModel>>
    {
        private readonly IEntryReadRepository _entryReadRepository;

        public EntryQueryHandler(IEntryReadRepository entryReadRepository)
        {
            _entryReadRepository = entryReadRepository;
        }

        public async Task<IEnumerable<EntryReadModel>> Handle(GetMonthEntriesQuery query, CancellationToken cancellationToken)
        {
            var entries = await _entryReadRepository.GetMonthEntries(query.UserId, query.Month);

            return entries.Select(e => new EntryReadModel
            {
                Id = e.EntryId,
                Description = e.Description,
                Value = e.Value,
                CategoryName = e.CategoryName,
                BankAccountName = e.BankAccountName,
                IsPaid = e.IsPaid,
                Date = e.Date
            });
        }

        public async Task<IEnumerable<MonthExpenseReadModel>> Handle(GetTotalExpensesGroupedByMonthQuery query, CancellationToken cancellationToken)
        {
            var expenses = await _entryReadRepository.GetAllExpensesByMonth(query.UserId, query.Month);

            var groupedByCategory = expenses.GroupBy(e => e.CategoryId);

            var result = new List<MonthExpenseReadModel>();
            var totalExpenses = expenses.Sum(e => e.Value).ToPositive();

            foreach (var expensesByCategory in groupedByCategory)
            {
                var readModel = new MonthExpenseReadModel();
                readModel.Category = expenses.FirstOrDefault(e => e.CategoryId == expensesByCategory.Key).CategoryName;

                readModel.TotalValue = expensesByCategory.Sum(e => e.Value).ToPositive();

                readModel.Percent = (readModel.TotalValue / totalExpenses * 100).To2DecimalPlaces();

                result.Add(readModel);
            }

            return result;
        }
    }
}
