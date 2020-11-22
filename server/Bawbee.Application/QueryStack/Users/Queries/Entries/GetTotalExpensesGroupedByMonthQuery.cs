using Bawbee.Application.QueryStack.Users.ReadModels.Expenses;
using Bawbee.Core.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;
using System.Collections.Generic;

namespace Bawbee.Application.QueryStack.Users.Queries.Entries
{
    public class GetTotalExpensesGroupedByMonthQuery : CommandQuery<IEnumerable<MonthExpenseReadModel>>
    {
        public int UserId { get; }
        public int Month { get; }

        public GetTotalExpensesGroupedByMonthQuery(int month, int userId)
        {
            UserId = userId;
            Month = month;
        }

        public override bool IsValid()
        {
            ValidationResult = new GetTotalExpensesGroupedByMonthValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class GetTotalExpensesGroupedByMonthValidation : AbstractValidator<GetTotalExpensesGroupedByMonthQuery>
    {
        public GetTotalExpensesGroupedByMonthValidation()
        {
            RuleFor(q => q)
                .Must(q => q.UserId.IsGreaterThanZero())
                .WithMessage("Invalid User");

            RuleFor(q => q)
                .Must(q => q.Month >= 1 && q.Month <= 12)
                .WithMessage("Invalid month");
        }
    }
}
