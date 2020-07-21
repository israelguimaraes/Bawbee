using FluentValidation;

namespace Bawbee.Application.Command.Users.Validators
{
    public class RegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
    {
        public const int PASSWORD_MIN_LENGTH = 6;
        public const int PASSWORD_MAX_LENGTH = 10;

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
                .WithMessage($"Password must be between {PASSWORD_MIN_LENGTH} and {PASSWORD_MAX_LENGTH} characters");
        }
    }
}
