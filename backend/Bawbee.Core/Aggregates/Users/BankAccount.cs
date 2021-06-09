using Bawbee.SharedKernel;

namespace Bawbee.Core.Aggregates.Users
{
    public class BankAccount : BaseEntity
    {
        private const decimal DEFAULT_ACCOUNT_INITIAL_VALUE = 3000;

        public string Name { get; private set; }
        public decimal InitialBalance { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        protected BankAccount() { }

        public BankAccount(string name, decimal initialBalance, int userId, int id = default)
        {
            Name = name;
            InitialBalance = initialBalance;
            UserId = userId;
            Id = id;
        }

        public static BankAccount CreateDefaultBankAccount(int userId)
        {
            return new BankAccount("Initial Account", DEFAULT_ACCOUNT_INITIAL_VALUE, userId);
        }

        public static decimal GetDefaultAccountInitialValue()
        {
            return DEFAULT_ACCOUNT_INITIAL_VALUE;
        }
    }
}
