using Bawbee.Mobile.Configs;
using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Models;
using Bawbee.Mobile.Models.Entries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bawbee.Mobile.Services
{
    public class EntryService
    {
        private static readonly string Endpoint = $"{AppConfiguration.BASE_URL}/api/v1/entries";

        private readonly HttpClient _httpClient;

        public EntryService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.UserAcessToken);
        }

        public async Task<bool> Add(Expense expense)
        {
            try
            {
                var json = JsonConvert.SerializeObject(expense);

                var response = await _httpClient.PostAsync(Endpoint, new StringContent(json, Encoding.UTF8, "application/json"));

                await HandleResponse(response);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(json);

                if (!apiResponse.IsSuccess)
                {
                    //apiResponse.Errors
                }

                throw new InvalidOperationException(json);
            }
        }
    }
}
