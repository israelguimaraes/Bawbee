using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Helpers.Extensions;
using Bawbee.Mobile.Services;
using Bawbee.Mobile.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService = new AuthService();

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                (LoginCommand as Command)?.ChangeCanExecute();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                (LoginCommand as Command)?.ChangeCanExecute();
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            Email = Settings.UserEmail;

            LoginCommand = new Command(async () =>
            {
                IsBusy = true;

                var responseAPI = await _authService.Login(Email, Password);

                if (responseAPI.Success)
                {
                    Settings.UserAcessToken = responseAPI.Data.AccessToken;
                    Settings.UserEmail = Email;

                    MessagingCenter.Send(this, MessageKey.OpenMainPage);
                }

                IsBusy = false;
            }, () =>
            {
                return Email.IsNotEmpty() && Password.IsNotEmpty();
            });
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(() =>
                {
                    MessagingCenter.Send(this, MessageKey.OpenRegisterPage);
                });
            }
        }

        public class MessageKey
        {
            public const string OpenLoginPage = nameof(OpenLoginPage);
            public const string OpenRegisterPage = nameof(OpenRegisterPage);
            public const string OpenMainPage = nameof(OpenMainPage);
        }
    }
}
