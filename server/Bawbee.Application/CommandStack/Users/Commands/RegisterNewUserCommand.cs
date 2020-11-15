using Bawbee.Application.Command.Users.Validators;
using Bawbee.Core.Commands;

namespace Bawbee.Application.CommandStack.Users.Commands
{
    public class RegisterNewUserCommand : BaseCommand
    {
        public string Name { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Password { get; }

        public RegisterNewUserCommand(string name, string lastName, string email, string password)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
