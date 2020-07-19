using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Services;
using Bawbee.Mobile.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService = new AuthService();

        public string Email { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            Email = Settings.UserEmail;
        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    var responseAPI = await _authService.Login(Email, Password);

                    if (responseAPI.IsSuccess)
                    {
                        Settings.UserAcessToken = responseAPI.Data.AccessToken;
                        Settings.UserEmail = Email;

                        MessagingCenter.Send(this, nameof(LoginCommand));
                    }

                    IsBusy = false;
                });
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(() =>
                {
                    MessagingCenter.Send(this, nameof(RegisterCommand));
                });
            }
        }
    }
}
