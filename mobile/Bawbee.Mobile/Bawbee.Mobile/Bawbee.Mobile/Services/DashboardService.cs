using Bawbee.Mobile.Configs;
using Bawbee.Mobile.Models;
using Bawbee.Mobile.Services.HttpRequestProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Bawbee.Mobile.Services
{
    public class DashboardService
    {
        private static readonly string Endpoint = $"{AppConfiguration.BASE_URL}/api/v1/expenses";

        private readonly RequestProvider _httpClient;

        public DashboardService()
        {
            _httpClient = new RequestProvider();
        }

        public async Task<ObservableCollection<BankAccount>> GetBankAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
