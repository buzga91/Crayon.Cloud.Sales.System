using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Integration.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.Cloud.Sales.Integration.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AccountDB> Account { get; set; }
        public DbSet<CustomerDB> Customer { get; set; }
        public DbSet<LicenseDB> License { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccountDB>()
          .HasOne(a => a.Customer)
          .WithMany(c => c.Accounts)
          .HasForeignKey(a => a.CustomerId)
          .OnDelete(DeleteBehavior.Cascade);

            // One-to-many relationship: An account can have many licenses
            modelBuilder.Entity<LicenseDB>()
                .HasOne(l => l.Account)
                .WithMany(a => a.Licenses)  // Account has many licenses
                .HasForeignKey(l => l.AccountId) // Foreign key for AccountId in LicenseDB
                .OnDelete(DeleteBehavior.Cascade); // Optional: 
        }
    }
}
