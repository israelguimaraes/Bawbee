using Bawbee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bawbee.Infra.Data.EF.Mappings
{
    public class EntryMapping : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(c => c.Description)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Value)
                .IsRequired();

            builder.Property(c => c.IsPaid)
                .IsRequired();

            builder.Property(c => c.Observations)
                .HasMaxLength(255);

            builder.Property(c => c.DateToPay)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            builder.HasOne(t => t.BankAccount)
                .WithMany()
                .HasForeignKey(t => t.BankAccountId);

            builder.HasOne(t => t.EntryCategory)
                .WithMany()
                .HasForeignKey(t => t.EntryCategoryId);
        }
    }
}
