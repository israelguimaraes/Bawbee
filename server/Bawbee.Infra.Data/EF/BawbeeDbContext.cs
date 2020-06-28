﻿using Bawbee.Domain.Entities;
using Bawbee.Infra.Data.EF.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Bawbee.Infra.Data.EF
{
    public class BawbeeDbContext : DbContext
    {
        public BawbeeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<EntryCategory> EntryCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new BankAccountMapping());
            modelBuilder.ApplyConfiguration(new EntryCategoryMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}