using Bawbee.Core.Commands;
using FluentValidation;

namespace Bawbee.Application.CommandStack.Users.Commands
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

    public class AddEntryCategoryCommandValidator : AbstractValidator<AddEntryCategoryCommand>
    {
        public AddEntryCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
}
