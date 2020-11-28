using Bawbee.Mobile.Configs;
using Bawbee.Mobile.Models.Dashboards;
using Bawbee.Mobile.Models.Entries;
using Bawbee.Mobile.ReadModels.Entries;
using Bawbee.Mobile.Services.HttpRequestProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bawbee.Mobile.Services.Entries
{
    public class ExpenseService
    {
        private static readonly string Endpoint = $"{AppConfiguration.BASE_URL}/api/v1/entries/expenses";

        private readonly RequestProvider _request;

        public ExpenseService()
        {
            _request = new RequestProvider();
        }

        public async Task<ObservableCollection<EntryReadModel>> GetCurrenthMonthEntries()
        {
            try
            {
                var entries = await _request.GetAsync<IEnumerable<EntryReadModel>>($"{Endpoint}/month/{DateTime.Now.Month}");

                return new ObservableCollection<EntryReadModel>(entries.OrderByDescending(e => e.Date));
            }
            catch (Exception ex)
            {
                return new ObservableCollection<EntryReadModel>();
            }
        }

        public async Task<bool> Add(Expense expense)
        {
            try
            {
                await _request.PostAsync(Endpoint, expense);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ObservableCollection<MonthExpense>> GetCurrentMonthExpenses()
        {
            try
            {
                var monthExpenses = await _request.GetAsync<IEnumerable<MonthExpense>>($"{Endpoint}/reports/month/{DateTime.Now.Month}");

                return new ObservableCollection<MonthExpense>(monthExpenses.OrderByDescending(e => e.TotalValue));
            }
            catch (Exception ex)
            {
                return new ObservableCollection<MonthExpense>();
            }
        }
    }
}
