using Bawbee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bawbee.Infra.Data.EF.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Ignore(t => t.Id);

            builder.HasKey(t => t.UserId);

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
