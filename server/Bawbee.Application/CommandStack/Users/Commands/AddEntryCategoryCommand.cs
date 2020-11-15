using Bawbee.Application.Command.Users.Validators;

namespace Bawbee.Application.CommandStack.Users.Commands
{
    public class AddEntryCategoryCommand : Core.Commands.BaseCommand
    {
        public string Name { get; }
        public int UserId { get; }

        public AddEntryCategoryCommand(string name, int userId)
        {
            Name = name;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddEntryCategoryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
