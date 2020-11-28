using Bawbee.Application.QueryStack.Users.ReadModels.Entries;
using Bawbee.Core.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;
using System.Collections.Generic;

namespace Bawbee.Application.QueryStack.Users.Queries.Entries
{
    public class GetMonthEntriesQuery : CommandQuery<IEnumerable<EntryReadModel>>
    {
        public int UserId { get; }
        public int Month { get; }

        public GetMonthEntriesQuery(int userId, int month)
        {
            UserId = userId;
            Month = month;
        }

        public override bool IsValid()
        {
            ValidationResult = new GetMonthEntriesValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class GetMonthEntriesValidation : AbstractValidator<GetMonthEntriesQuery>
    {
        public GetMonthEntriesValidation()
        {
            RuleFor(q => q)
                .Must(q => q.UserId.IsGreaterThanZero())
                .WithMessage("Invalid User");
        }
    }
}
