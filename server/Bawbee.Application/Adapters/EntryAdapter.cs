﻿using Bawbee.Application.CommandStack.Expenses.Commands;
using Bawbee.Application.CommandStack.Expenses.InputModels;
using Bawbee.Domain.AggregatesModel.Entries;
using Bawbee.Domain.Events.Entries;

namespace Bawbee.Application.Adapters
{
    public static class EntryAdapter
    {
        #region Map to Domain

        public static Expense MapToDomain(this CreateExpenseCommand command)
        {
            return new Expense(
                command.Description, command.Value, command.IsPaid,
                command.Observations, command.DateToPay, command.UserId,
                command.BankAccountId, command.CategoryId, command.EntryId);
        }

        public static Expense MapToDomain(this UpdateExpenseCommand command)
        {
            return new Expense(
                command.Description, command.Value, command.IsPaid,
                command.Observations, command.DateToPay, command.UserId,
                command.BankAccountId, command.CategoryId, command.EntryId);
        }

        #endregion

        #region Map to Commands

        public static CreateExpenseCommand MapToCreateExpenseCommand(this CreateExpenseInputModel inputModel, int userId)
        {
            return new CreateExpenseCommand(
                inputModel.Description, inputModel.Value,
                inputModel.IsPaid, inputModel.Observations, inputModel.DateToPay,
                userId, inputModel.BankAccountId, inputModel.CategoryId);
        }

        public static UpdateExpenseCommand MapToUpdateExpenseCommand(this UpdateExpenseInputModel inputModel, int userId)
        {
            return new UpdateExpenseCommand(
                inputModel.EntryId, inputModel.Description, inputModel.Value,
                inputModel.IsPaid, inputModel.Observations, inputModel.DateToPay,
                userId, inputModel.BankAccountId, inputModel.CategoryId);
        }

        #endregion

        #region Map to Events

        public static ExpenseCreatedEvent MapToExpenseCreatedEvent(this Expense entity)
        {
            return new ExpenseCreatedEvent(
                entity.Id, entity.Description, entity.Value,
                entity.IsPaid, entity.Observations, entity.DateToPay,
                entity.UserId, entity.BankAccountId, entity.CategoryId);
        }

        public static ExpenseUpdatedEvent MapToExpenseUpdatedEvent(this Expense entity)
        {
            return new ExpenseUpdatedEvent(
                entity.Id, entity.Description, entity.Value,
                entity.IsPaid, entity.Observations, entity.DateToPay,
                entity.UserId, entity.BankAccountId, entity.CategoryId);
        }

        public static ExpenseDeletedEvent MapToExpenseDeletedEvent(this Expense entity)
        {
            return new ExpenseDeletedEvent(
                entity.Id, entity.Description, entity.Value,
                entity.IsPaid, entity.Observations, entity.DateToPay,
                entity.UserId, entity.BankAccountId, entity.CategoryId);
        }

        #endregion
    }
}