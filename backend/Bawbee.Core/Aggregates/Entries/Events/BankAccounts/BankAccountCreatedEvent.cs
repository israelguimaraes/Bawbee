using Bawbee.SharedKernel;

namespace Bawbee.Core.Aggregates.Entries.Events.BankAccounts
{
    public class BankAccountCreatedEvent : BaseEvent
    {
        public int BankAccountId { get; }
        public string Name { get; }
        public decimal InitialBalance { get; }
        public int UserId { get; }

        public BankAccountCreatedEvent(int bankAccountId, string name, decimal initialBalance, int userId)
        {
            BankAccountId = bankAccountId;
            Name = name;
            InitialBalance = initialBalance;
            UserId = userId;
        }
    }
}
