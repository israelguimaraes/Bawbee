using Bawbee.Domain.Commands.Users.Commands;
using FluentValidation;

namespace Bawbee.Application.Users.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Email)
                   .NotEmpty()
                   .WithMessage("E-mail is required")
                   .EmailAddress()
                   .WithMessage("E-mail is invalid");

            RuleFor(c => c.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
