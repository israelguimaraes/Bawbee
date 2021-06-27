using Bawbee.Core.Aggregates.Entries.Shared;
using Bawbee.Core.Aggregates.Users;
using Bawbee.Infrastructure.Persistence.SqlServer.EFCore.Contexts.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Bawbee.Infrastructure.Persistence.SqlServer.EFCore.Contexts
{
    public class BawbeeDbContext : DbContext
    {
        public BawbeeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BaseEntry> Entries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BankConfiguration());
            modelBuilder.ApplyConfiguration(new EntryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
