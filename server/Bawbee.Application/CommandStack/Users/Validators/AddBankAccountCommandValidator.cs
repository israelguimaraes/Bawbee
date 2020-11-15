using Bawbee.Application.CommandStack.Users.Commands;
using FluentValidation;

namespace Bawbee.Application.Command.Users.Validators
{
    public class AddBankAccountCommandValidator : AbstractValidator<AddBankAccountCommand>
    {
        public AddBankAccountCommandValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty()
               .WithMessage("Name is required");
        }
    }
}
