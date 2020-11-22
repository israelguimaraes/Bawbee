using Bawbee.Application.QueryStack.Users.ReadModels;
using Bawbee.Core.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;
using System.Collections.Generic;

namespace Bawbee.Application.QueryStack.Users.Queries
{
    public class GetAllBankAccountsByUserQuery : CommandQuery<IEnumerable<BankAccountReadModel>>
    {
        public int UserId { get; }

        public GetAllBankAccountsByUserQuery(int userId)
        {
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new GetAllBankAccountsByUserValidation().Validate(this);
           return ValidationResult.IsValid;
        }
    }

    public class GetAllBankAccountsByUserValidation : AbstractValidator<GetAllBankAccountsByUserQuery>
    {
        public GetAllBankAccountsByUserValidation()
        {
            RuleFor(q => q)
                .Must(q => q.UserId.IsGreaterThanZero())
                .WithMessage("Invalid User");
        }
    }
}
