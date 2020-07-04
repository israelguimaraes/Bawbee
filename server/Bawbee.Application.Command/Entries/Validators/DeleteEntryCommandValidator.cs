using Bawbee.Infra.CrossCutting.Common.Extensions;
using FluentValidation;

namespace Bawbee.Application.Command.Entries.Validators
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
