using Bawbee.Mobile.InputModels.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Bawbee.Mobile.Services.Auth
{
    public class AuthService
    {
        public async Task<bool> Register(string email, string name, string lastName, string password, string confirmPassword)
        {
            var client = new HttpClient();

            var model = new RegisterNewUserInputModel
            {
                Email = email,
                Name = name,
                LastName = lastName,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //var endpoint = $"https://localhost:5001/api/v1/auth/register";
            var endpoint = $"https://localhost:44310/api/v1/Auth/register";

            var response = await client.PostAsync(endpoint, content);

            return response.IsSuccessStatusCode;
        }
    }
}
