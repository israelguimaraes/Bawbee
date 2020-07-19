using Bawbee.Domain.Core.Events;

namespace Bawbee.Domain.Events.BankAccounts
{
    public class BankAccountAddedEvent : Event
    {
        public int BankAccountId { get; }
        public string Name { get; }
        public decimal InitialBalance { get; }
        public int UserId { get; }

        public BankAccountAddedEvent(int bankAccountId, string name, decimal initialBalance, int userId)
        {
            BankAccountId = bankAccountId;
            Name = name;
            InitialBalance = initialBalance;
            UserId = userId;
        }
    }
}
