using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Services.Auth;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Auth
{
    public class RegisterNewUserViewModel
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
                    var isSuccess = await _authService.Register(Email, Name, LastName, Password, ConfirmPassword);

                    Settings.UserEmail = Email;
                    Settings.UserPassword = Password;

                    Message = isSuccess ? "Registered ok" : "Fail...";
                });
            }
        }
    }
}
