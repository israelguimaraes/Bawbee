using Bawbee.Application.CommandStack.Entries.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;

namespace Bawbee.Application.CommandStack.Entries.Validators
{
    public class DeleteEntryCommandValidator : AbstractValidator<DeleteEntryCommand>
    {
        public DeleteEntryCommandValidator()
        {
            RuleFor(c => c.EntryId)
                   .Must(c => c.IsGreaterThanZero())
                   .WithMessage($"{nameof(NewEntryCommand.EntryId)} is invalid");

            RuleFor(c => c.UserId)
                   .Must(c => c.IsGreaterThanZero())
                   .WithMessage($"{nameof(NewEntryCommand.UserId)} is invalid");
        }
    }
}
