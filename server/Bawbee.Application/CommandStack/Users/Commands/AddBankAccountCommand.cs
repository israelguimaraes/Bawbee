﻿using Bawbee.Core.Commands;
using FluentValidation;

namespace Bawbee.Application.CommandStack.Users.Commands
{
    public class AddBankAccountCommand : BaseCommand
    {
        public string Name { get; }
        public decimal InitialBalance { get; }
        public int UserId { get; }

        public AddBankAccountCommand(string name, decimal initialBalance, int userId)
        {
            Name = name;
            InitialBalance = initialBalance;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddBankAccountCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddBankAccountCommandValidator : AbstractValidator<AddBankAccountCommand>
    {
        public AddBankAccountCommandValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty()
               .WithMessage("Name is required");
        }
    }
}
