using Bawbee.Application.Command.Users.Validators;

namespace Bawbee.Application.CommandStack.Users.Commands
{
    public class AddBankAccountCommand : Core.Commands.BaseCommand
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
