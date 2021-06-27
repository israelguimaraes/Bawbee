using Bawbee.Application.Mediator;

namespace Bawbee.Application.UseCases.Users.CreateUser
{
    public class CreateUserCommand : BaseCommand
    {
        public string Name { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }

        public CreateUserCommand(string name, string lastName, string email, string password, string confirmPassword)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
