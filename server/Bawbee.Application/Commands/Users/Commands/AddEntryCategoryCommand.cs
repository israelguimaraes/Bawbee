using Bawbee.Application.Command.Users.Validators;
using Bawbee.Domain.Core.Commands;

namespace Bawbee.Application.Command.Users.Categories
{
    public class AddEntryCategoryCommand : BaseCommand
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
