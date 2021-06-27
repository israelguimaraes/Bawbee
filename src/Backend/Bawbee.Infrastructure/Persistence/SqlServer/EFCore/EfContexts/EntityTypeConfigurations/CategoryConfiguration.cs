using Bawbee.Core.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bawbee.Infrastructure.Persistence.Sql.EfContexts.EntityTypeConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Ignore(t => t.DomainEvents);

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
