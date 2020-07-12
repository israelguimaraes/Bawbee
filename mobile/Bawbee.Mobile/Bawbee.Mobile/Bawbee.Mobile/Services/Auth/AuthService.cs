using Bawbee.Mobile.Models;
using Bawbee.Mobile.Models.Users;
using Bawbee.Mobile.ViewModels.Auth;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bawbee.Mobile.Services.Auth
{
    public class AuthService
    {
        private static string BASE_URL = App.IsAndroid ? "http://10.0.2.2:5000/api/v1/auth" : "http://localhost:5000/api/v1/auth";
        //private static string UserAcessToken = Settings.UserAcessToken;

        private HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();

            //if (!string.IsNullOrWhiteSpace(UserAcessToken))
            //{
            //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserAcessToken);
            //}
        }

        public async Task<bool> Register(string email, string name, string lastName, string password, string confirmPassword)
        {
            var model = new RegisterNewUserViewModel
            {
                Email = email,
                Name = name,
                LastName = lastName,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            try
            {
                var response = await _httpClient.PostAsync($"{BASE_URL}/register", new StringContent(json, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ApiResponse<UserAccessToken>> Login(string email, string password)
        {
            try
            {
                var jsonObject = JsonConvert.SerializeObject(new LoginViewModel
                {
                    Email = email,
                    Password = password
                });

                var httpReponse = await _httpClient.PostAsync($"{BASE_URL}/login", new StringContent(jsonObject, Encoding.UTF8, "application/json"));

                var jsonResponse = await httpReponse.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<UserAccessToken>>(jsonResponse);

                return apiResponse;
            }
            catch (Exception ex)
            {
                // TODO: ...
                return new ApiResponse<UserAccessToken> { IsSuccess = false };
            }
        }
    }
}

