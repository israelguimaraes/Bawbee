using Bawbee.Domain.AggregatesModel.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bawbee.Infra.Data.SQLServer.Mappings
{
    public class BankAccountMapping : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(t => t.Id);

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
