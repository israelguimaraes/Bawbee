using Bawbee.Core.Aggregates.Users;
using FluentValidation;

namespace Bawbee.Application.UseCases.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
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
                .WithMessage($"Password must be between {User.PasswordMinLength} and {User.PasswordMaxLength} characters");
        }
    }
}
