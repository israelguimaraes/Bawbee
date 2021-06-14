using Bawbee.Core.Aggregates.Entries;
using Bawbee.Core.Aggregates.Entries.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bawbee.Infrastructure.Persistence.Sql.EfContexts.Mappings
{
    public class EntryMapping : IEntityTypeConfiguration<BaseEntry>
    {
        public void Configure(EntityTypeBuilder<BaseEntry> builder)
        {
            builder.HasKey(t => t.Id);

            #region Discriminator - Expenses and Incomes

            builder
                .HasDiscriminator<string>("Type")
                .HasValue<Expense>(nameof(Expense))
                .HasValue<Income>(nameof(Income));

            builder.Property("Type")
                .HasColumnType("CHAR(7)");

            #endregion

            builder.Property(c => c.Description)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Value)
                .IsRequired();

            builder.Property(c => c.IsPaid)
                .IsRequired();

            builder.Property(c => c.Observations)
                .HasMaxLength(255);

            builder.Property(c => c.Date)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            builder.HasOne(t => t.BankAccount)
                .WithMany()
                .HasForeignKey(t => t.BankAccountId);

            builder.HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId);
        }
    }
}
