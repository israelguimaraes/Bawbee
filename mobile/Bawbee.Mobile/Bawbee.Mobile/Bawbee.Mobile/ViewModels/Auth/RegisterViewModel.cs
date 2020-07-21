using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Helpers.Extensions;
using Bawbee.Mobile.Services;
using Bawbee.Mobile.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Auth
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        public RegisterViewModel()
        {
            _authService = new AuthService();

            RegisterCommand = new Command(async () =>
            {
                IsBusy = true;
                var isSuccess = await _authService.Register(Email, Name, LastName, Password, ConfirmPassword);

                if (isSuccess)
                {
                    Settings.UserEmail = Email;
                    MessagingCenter.Send(this, MessageKey.UserRegistered);
                }
                IsBusy = false;
            }, () =>
            {
                return Email.IsNotEmpty() &&
                       Name.IsNotEmpty() &&
                       LastName.IsNotEmpty() &&
                       Password.IsNotEmpty() &&
                       ConfirmPassword.IsNotEmpty();
            });
        }

        public ICommand RegisterCommand { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        public class MessageKey
        {
            public const string UserRegistered = nameof(UserRegistered);
        }
    }
}
