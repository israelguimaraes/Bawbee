using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Services;
using Bawbee.Mobile.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Auth
{
    public class RegisterNewUserViewModel : BaseViewModel
    {
        private AuthService _authService = new AuthService();

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Message { get; set; }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    var isSuccess = await _authService.Register(Email, Name, LastName, Password, ConfirmPassword);

                    if (isSuccess)
                    {
                        Settings.UserEmail = Email;
                        MessagingCenter.Send(this, MessageKey.UserRegistered);
                    }
                    IsBusy = false;
                });
            }
        }

        public class MessageKey
        {
            public const string UserRegistered = nameof(RegisterCommand);
        }
    }
}
