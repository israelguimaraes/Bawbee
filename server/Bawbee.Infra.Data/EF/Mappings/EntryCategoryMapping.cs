using Bawbee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bawbee.Infra.Data.EF.Mappings
{
    public class EntryCategoryMapping : IEntityTypeConfiguration<EntryCategory>
    {
        public void Configure(EntityTypeBuilder<EntryCategory> builder)
        {
            builder.Ignore(t => t.Id);

            builder.HasKey(t => t.EntryCategoryId);

            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(t => t.EntryCategories)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
