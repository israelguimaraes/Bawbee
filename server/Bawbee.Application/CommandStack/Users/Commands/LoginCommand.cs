using Bawbee.Application.Command.Users.Validators;

namespace Bawbee.Application.CommandStack.Users.Commands
{
    public class LoginCommand : Core.Commands.BaseCommand
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
