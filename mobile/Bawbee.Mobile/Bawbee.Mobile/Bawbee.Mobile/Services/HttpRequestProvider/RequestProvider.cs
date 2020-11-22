using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Helpers.Extensions;
using Bawbee.Mobile.Models.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bawbee.Mobile.Services.HttpRequestProvider
{
    public class RequestProvider
    {
        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            var httpClient = CreateHttpClient();
            var response = await httpClient.GetAsync(uri);

            await HandleResponse(response);
            
            var json = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(json));

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data)
        {
            var httpClient = CreateHttpClient();

            var jsonData = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync(uri, content);

            await HandleResponse(response);

            var json = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(json));

            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, TResult data)
        {
            var httpClient = CreateHttpClient();

            var jsonData = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PutAsync(uri, content);

            await HandleResponse(response);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(jsonResponse));

            return result;
        }

        private HttpClient CreateHttpClient()
        {
            var token = Settings.UserAcessToken;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (token.IsNotEmpty())
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            return httpClient;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    MessagingCenter.Send(nameof(ServiceAuthenticationException), nameof(ServiceAuthenticationException));
                }

                MessagingCenter.Send(nameof(HttpRequestException), nameof(HttpRequestException));
            }
        }
    }
}
