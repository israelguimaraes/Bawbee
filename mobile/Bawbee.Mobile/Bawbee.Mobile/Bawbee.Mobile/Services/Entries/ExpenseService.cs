using Bawbee.Mobile.Configs;
using Bawbee.Mobile.Models;
using Bawbee.Mobile.Models.Dashboards;
using Bawbee.Mobile.Models.Entries;
using Bawbee.Mobile.ReadModels.Entries;
using Bawbee.Mobile.Services.HttpRequestProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Bawbee.Mobile.Services.Entries
{
    public class ExpenseService
    {
        private static readonly string Endpoint = $"{AppConfiguration.BASE_URL}/api/v1/expenses";

        private readonly RequestProvider _request;

        public ExpenseService()
        {
            _request = new RequestProvider();
        }

        public async Task<ObservableCollection<EntryReadModel>> GetEntries()
        {
            try
            {
                var response = await _request.GetAsync<ApiResponse<IEnumerable<EntryReadModel>>>(Endpoint);

                return new ObservableCollection<EntryReadModel>(response.Data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Add(Expense expense)
        {
            try
            {
                var json = JsonConvert.SerializeObject(expense);

                await _request.PostAsync(Endpoint, expense);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ObservableCollection<MonthExpense>> GetCurrentMonthExpenses()
        {
            var monthExpenses = await _request.GetAsync<IEnumerable<MonthExpense>>($"{Endpoint}/month/{DateTime.Now.Month}");

            return new ObservableCollection<MonthExpense>(monthExpenses);
        }
    }
}
