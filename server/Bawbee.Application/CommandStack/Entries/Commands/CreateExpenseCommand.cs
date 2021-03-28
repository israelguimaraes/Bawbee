using Bawbee.Application.CommandStack.Entries.Validators;
using Bawbee.Core.Commands;
using System;

namespace Bawbee.Application.CommandStack.Entries.Commands
{
    public class CreateExpenseCommand : EntryCommand
    {
        public CreateExpenseCommand(
            string description,
            decimal value,
            bool isPaid,
            string observations,
            DateTime dateToPay,
            int userId,
            int bankAccountId,
            int categoryId,
            int entryId = 0)
            : base(description, value, isPaid, observations, dateToPay, userId, bankAccountId, categoryId, entryId)
        {

        }

        public override OperationEnum OperationResult => OperationEnum.

        public override bool IsValid()
        {
            ValidationResult = new AddExpenseCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
