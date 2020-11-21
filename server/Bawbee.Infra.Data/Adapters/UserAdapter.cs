using Bawbee.Domain.Events;
using Bawbee.Infra.Data.Documents;
using Bawbee.Infra.Data.Documents.Users;

namespace Bawbee.Infra.Data.Adapters
{
    public static class UserAdapter
    {
        public static UserDocument MapToUserDocument(this UserRegisteredEvent @event)
        {
            var document = new UserDocument();

            document.UserId = @event.UserId;
            document.Name = @event.Name;
            document.LastName = @event.LastName;
            document.Email = @event.Email;
            document.Password = @event.Password;
            document.CreatedAt = @event.Timestamp;

            foreach (var b in @event.BankAccounts)
            {
                document.BankAccounts.Add(new BankAccount
                {
                    BankAccountId = b.Id,
                    InitialBalance = b.InitialBalance,
                    Name = b.Name,
                    CreatedAt = b.CreatedAt
                });
            }

            foreach (var c in @event.Categories)
            {
                document.Categories.Add(new Category
                {
                    CategoryId = c.Id,
                    Name = c.Name,
                    CreatedAt = c.CreatedAt
                });
            }

            return document;
        }
    }
}
