using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.ViewModels.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bawbee.Mobile.Services.Auth
{
    public class AuthService
    {
        private static string BASE_URL = Device.RuntimePlatform == Device.Android ? "http://10.0.2.2:5000/api/v1/" : "http://localhost:5000/api/v1/";
        private static string UserAcessToken = Settings.UserAcessToken;

        private HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAcessToken);
            _httpClient.BaseAddress = new Uri($"{BASE_URL}/auth/");
        }

        public async Task<bool> Register(string email, string name, string lastName, string password, string confirmPassword)
        {
            const string endpoint = "register";

            var model = new RegisterNewUserViewModel
            {
                Email = email,
                Name = name,
                LastName = lastName,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await _httpClient.PostAsync(endpoint, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> Login(string email, string password)
        {
            const string endpoint = "login";

            var model = new LoginViewModel
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await _httpClient.PostAsync(endpoint, content);

                return "";
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
