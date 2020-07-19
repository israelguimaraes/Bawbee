using Bawbee.Application.Command.Users.BankAccounts;
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
