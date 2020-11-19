using Bawbee.Domain.AggregatesModel.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bawbee.Infra.Data.EF.Mappings
{
    public class EntryCategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(t => t.Categories)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
