using Bawbee.Application.CommandStack.Users.Commands;
using FluentValidation;

namespace Bawbee.Application.Command.Users.Validators
{
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
