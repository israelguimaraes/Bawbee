using Bawbee.Domain.Core.Models;

namespace Bawbee.Application.Query.Users.Documents
{
    public class BankAccountDocument : BaseEntity
    {
        public int BankAccountId { get; set; }
        public string Name { get; set; }
        public decimal InitialBalance { get; set; }
    }
}