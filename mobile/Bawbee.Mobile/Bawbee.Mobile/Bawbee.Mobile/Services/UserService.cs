using Bawbee.Mobile.Configs;
using Bawbee.Mobile.Models;
using Bawbee.Mobile.Models.Exceptions;
using Bawbee.Mobile.Services.HttpRequestProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bawbee.Mobile.Services
{
    public class UserService
    {
        private static readonly string Endpoint = $"{AppConfiguration.BASE_URL}/api/v1/users";
        private static readonly string CategoriesEndpoint = $"{Endpoint}/categories";
        private static readonly string BankAccountsEndpoint = $"{Endpoint}/bank-accounts";

        private readonly RequestProvider _httpClient;

        public UserService()
        {
            _httpClient = new RequestProvider();
        }

        public async Task<ObservableCollection<BankAccount>> GetBankAccounts()
        {
            try
            {
                var bankAccounts = await _httpClient.GetAsync<IEnumerable<BankAccount>>(BankAccountsEndpoint);

                return new ObservableCollection<BankAccount>(bankAccounts);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ObservableCollection<EntryCategory>> GetCategories()
        {
            try
            {
                var categories = await _httpClient.GetAsync<IEnumerable<EntryCategory>>(CategoriesEndpoint);

                return new ObservableCollection<EntryCategory>(categories);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var exception = new ServiceAuthenticationException(content);

                    MessagingCenter.Send(exception, nameof(ServiceAuthenticationException));
                }

                throw new InvalidOperationException();
            }
        }
    }
}
