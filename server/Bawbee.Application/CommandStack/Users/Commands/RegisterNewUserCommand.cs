using Bawbee.Core.Commands;
using Bawbee.Domain.Entities;
using FluentValidation;

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

    public class RegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
    {
        public RegisterNewUserCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("Last name is required");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("E-mail is required")
                .EmailAddress()
                .WithMessage("E-mail is invalid");

            RuleFor(c => c.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .Length(6, 10)
                .WithMessage($"Password must be between {User.PASSWORD_MIN_LENGTH} and {User.PASSWORD_MAX_LENGTH} characters");
        }
    }
}
