using Bawbee.Application.CommandStack.Entries.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;
using System;

namespace Bawbee.Application.CommandStack.Entries.Validators
{
    public class UpdateEntryCommandValidator : AbstractValidator<UpdateEntryCommand>
    {
        public UpdateEntryCommandValidator()
        {
            RuleFor(c => c.EntryId)
                   .Must(c => c.IsGreaterThanZero())
                   .WithMessage($"{nameof(NewEntryCommand.EntryId)} is invalid");

            RuleFor(c => c.UserId)
                   .Must(c => c.IsGreaterThanZero())
                   .WithMessage($"{nameof(NewEntryCommand.UserId)} is invalid");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage($"{nameof(NewEntryCommand.Description)} is required");

            RuleFor(c => c.Value)
                .NotEmpty()
                .WithMessage($"{nameof(NewEntryCommand.Value)} is required");

            RuleFor(c => c.IsPaid)
               .NotEmpty()
               .WithMessage($"{nameof(NewEntryCommand.IsPaid)} is required");

            RuleFor(c => c.DateToPay)
               .Must(c => c != DateTime.MinValue)
               .WithMessage($"{nameof(NewEntryCommand.DateToPay)} is required");

            RuleFor(c => c.BankAccountId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(NewEntryCommand.BankAccountId)} is invalid");

            RuleFor(c => c.EntryCategoryId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(NewEntryCommand.EntryCategoryId)} is invalid");
        }
    }
}
