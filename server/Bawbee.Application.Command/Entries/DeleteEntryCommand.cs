using Bawbee.Application.Command.Entries.Validators;
using Bawbee.Domain.Core.Commands;

namespace Bawbee.Application.Command.Entries
{
    public class DeleteEntryCommand : BaseCommand
    {
        public int EntryId { get; }
        public int UserId { get; set; }

        public DeleteEntryCommand(int entryId, int userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteEntryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
