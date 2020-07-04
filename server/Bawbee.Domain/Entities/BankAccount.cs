using Bawbee.Domain.Core.Models;
using Newtonsoft.Json;

namespace Bawbee.Domain.Entities
{
    public class BankAccount : BaseEntity
    {
        public const decimal DEFAULT_ACCOUNT_INITIAL_VALUE = 1234567.89m;

        public int BankAccountId { get; private set; }
        public string Name { get; private set; }
        public decimal InitialBalance { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        [JsonConstructor]
        public BankAccount(string name, decimal initialBalance, int userId, int bankAccountId = default)
        {
            Name = name;
            InitialBalance = initialBalance;
            UserId = userId;
            BankAccountId = bankAccountId;
        }

        public static BankAccount CreateDefaultBankAccount(int userId)
        {
            return new BankAccount("Default Bank Account", DEFAULT_ACCOUNT_INITIAL_VALUE, userId);
        }
    }
}
