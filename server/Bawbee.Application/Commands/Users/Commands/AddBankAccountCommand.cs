using Bawbee.Application.Command.Users.Validators;
using Bawbee.Domain.Core.Commands;

namespace Bawbee.Application.Command.Users.BankAccounts
{
    public class AddBankAccountCommand : BaseCommand
    {
        public string Name { get; }
        public decimal InitialBalance { get; }
        public int UserId { get; }

        public AddBankAccountCommand(string name, decimal initialBalance, int userId)
        {
            Name = name;
            InitialBalance = initialBalance;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddBankAccountCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
