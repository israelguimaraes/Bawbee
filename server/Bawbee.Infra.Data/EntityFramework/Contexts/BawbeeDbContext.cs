using Bawbee.Domain.Entities;
using Bawbee.Infra.Data.EntityFramework.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bawbee.Infra.Data.EntityFramework.Contexts
{
    public class BawbeeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryCategory> EntryCategories { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<UserContext> UserContexts { get; set; }

        public BawbeeDbContext(DbContextOptions<BawbeeDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
