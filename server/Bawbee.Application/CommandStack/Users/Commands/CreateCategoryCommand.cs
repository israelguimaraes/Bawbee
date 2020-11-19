using Bawbee.Core.Commands;
using FluentValidation;

namespace Bawbee.Application.CommandStack.Users.Commands
{
    public class CreateCategoryCommand : BaseCommand
    {
        public string Name { get; }
        public int UserId { get; }

        public CreateCategoryCommand(string name, int userId)
        {
            Name = name;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateCategoryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
}
