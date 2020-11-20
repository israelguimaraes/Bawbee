using Bawbee.Domain.AggregatesModel.Entries;
using Bawbee.Domain.AggregatesModel.Users;
using Bawbee.Infra.Data.SQLServer.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Bawbee.Infra.Data.SQLServer
{
    public class BawbeeDbContext : DbContext
    {
        public BawbeeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Category> EntryCategories { get; set; }
        public DbSet<Entry> Entries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new BankAccountMapping());
            modelBuilder.ApplyConfiguration(new EntryMapping());
            modelBuilder.ApplyConfiguration(new EntryCategoryMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
