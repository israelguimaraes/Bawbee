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

        private readonly RequestProvider _request;

        public UserService()
        {
            _request = new RequestProvider();
        }

        public async Task<ObservableCollection<BankAccount>> GetBankAccounts()
        {
            try
            {
                var bankAccounts = await _request.GetAsync<IEnumerable<BankAccount>>(BankAccountsEndpoint);

                return new ObservableCollection<BankAccount>(bankAccounts);
            }
            catch (Exception ex)
            {
                return new ObservableCollection<BankAccount>();
            }
        }

        public async Task<ObservableCollection<Category>> GetCategories()
        {
            try
            {
                var categories = await _request.GetAsync<IEnumerable<Category>>(CategoriesEndpoint);

                return new ObservableCollection<Category>(categories);
            }
            catch (Exception ex)
            {
                return new ObservableCollection<Category>();
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
