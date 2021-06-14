using Bawbee.Core.Aggregates.Entries.Shared;
using Bawbee.Core.Aggregates.Users;
using Bawbee.Infrastructure.Persistence.Sql.EfContexts.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Bawbee.Infrastructure.Persistence.Sql.EfContexts
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
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new BankAccountMapping());
            modelBuilder.ApplyConfiguration(new EntryMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
