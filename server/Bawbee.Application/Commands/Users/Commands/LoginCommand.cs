using Bawbee.Application.Command.Users.Validators;
using Bawbee.Domain.Core.Commands;

namespace Bawbee.Application.Command.Users
{
    public class LoginCommand : BaseCommand
    {
        public string Email { get; }
        public string Password { get; }

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new LoginCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
