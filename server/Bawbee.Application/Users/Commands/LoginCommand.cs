using Bawbee.Application.Users.Validators;
using Bawbee.Domain.Commands.Users.Validators;
using Bawbee.Domain.Core.Commands;

namespace Bawbee.Application.Users.Commands
{
    public class LoginCommand : Command
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
