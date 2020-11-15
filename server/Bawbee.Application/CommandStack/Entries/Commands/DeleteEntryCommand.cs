using Bawbee.Application.CommandStack.Entries.Validators;

namespace Bawbee.Application.CommandStack.Entries.Commands
{
    public class DeleteEntryCommand : Core.Commands.BaseCommand
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
