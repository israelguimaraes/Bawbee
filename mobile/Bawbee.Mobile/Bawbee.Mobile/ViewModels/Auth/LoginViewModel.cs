using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Services.Auth;
using Bawbee.Mobile.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Auth
{
    public class LoginViewModel
    {
        private readonly AuthService _authService = new AuthService();

        public string Email { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var responseAPI = await _authService.Login(Email, Password);

                    if (responseAPI.IsSuccess)
                    {
                        Settings.UserAcessToken = responseAPI.Data.AccessToken;

                        await NavigationHelper.PushAsync(new DashboardPage());
                    }
                });
            }
        }

        public LoginViewModel()
        {
            Email = Settings.UserEmail;
            Password = Settings.UserPassword;
        }
    }
}
