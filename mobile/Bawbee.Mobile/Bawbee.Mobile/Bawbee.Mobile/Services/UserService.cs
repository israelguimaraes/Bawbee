using Bawbee.Mobile.Configs;
using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Models;
using Bawbee.Mobile.Models.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bawbee.Mobile.Services
{
    public class UserService
    {
        private static readonly string Endpoint = $"{AppConfiguration.BASE_URL}/api/v1/users";
        private static readonly string CategoriesEndpoint = $"{Endpoint}/categories";
        private static readonly string BankAccountsEndpoint = $"{Endpoint}/bank-accounts";

        private readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.UserAcessToken);
        }

        public async Task<ObservableCollection<BankAccount>> GetBankAccounts()
        {
            try
            {
                var response = await _httpClient.GetAsync(BankAccountsEndpoint);

                await HandleResponse(response);

                var json = await response.Content.ReadAsStringAsync();
                var bankAccounts = JsonConvert.DeserializeObject<IEnumerable<BankAccount>>(json);

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
                var response = await _httpClient.GetAsync(CategoriesEndpoint);

                await HandleResponse(response);

                var json = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<IEnumerable<EntryCategory>>(json);

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
