using Bawbee.Core.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bawbee.Infrastructure.Persistence.Sql.EfContexts.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Password)
               .HasMaxLength(255)
               .IsRequired();
        }
    }
}
