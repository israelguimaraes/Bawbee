using Bawbee.Domain.Core.Models;

namespace Bawbee.Domain.Entities
{
    public class BankAccount : BaseEntity
    {
        public int BankAccountId { get; private set; }
        public string Name { get; private set; }
        public decimal InitialBalance { get; private set; }
        public int UserContextId { get; private set; }

        public UserContext UserContext { get; private set; }

        public BankAccount(string name, decimal initialBalance, int userContextId)
        {
            Name = name;
            InitialBalance = initialBalance;
            UserContextId = userContextId;
        }
    }
}
