using Bawbee.Mobile.InputModels.Auth;
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


            var baseUrl = "";

            if (Device.RuntimePlatform == Device.Android)
            {
                baseUrl = "http://10.0.2.2:5000/api/v1/";
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                baseUrl = "http://localhost:5000/api/v1/";
            }

            var endpoint = $"auth/register";

            var finalEndpoint = baseUrl + endpoint;

            try
            {
                var response = await client.PostAsync(finalEndpoint, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
