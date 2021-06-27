using Bawbee.Core.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bawbee.Infrastructure.Persistence.Sql.EfContexts.EntityTypeConfigurations
{
    public class BankConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Ignore(t => t.DomainEvents);

            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.InitialBalance)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(t => t.BankAccounts)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
