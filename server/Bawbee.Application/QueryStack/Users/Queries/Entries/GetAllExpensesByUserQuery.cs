using Bawbee.Application.QueryStack.Users.ReadModels.Expenses;
using Bawbee.Core.Commands;
using FluentValidation;
using System.Collections.Generic;

namespace Bawbee.Application.QueryStack.Users.Queries.Entries
{
    public class GetAllExpensesByUserQuery : CommandQuery<IEnumerable<ExpenseReadModel>>
    {
        public int UserId { get; }

        public GetAllExpensesByUserQuery(int userId)
        {
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new GetAllExpensesByUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class GetAllExpensesByUserValidation : AbstractValidator<GetAllExpensesByUserQuery>
    {
        public GetAllExpensesByUserValidation()
        {
            RuleFor(q => q)
                .Must(q => q.UserId <= 0)
                .WithMessage("Invalid User");
        }
    }
}
