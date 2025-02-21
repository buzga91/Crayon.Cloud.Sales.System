using Crayon.Cloud.Sales.Integration.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crayon.Cloud.Sales.Integration.ContextDB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AccountDB> Accounts { get; set; }
        public DbSet<CustomerDB> Customers { get; set; }
        public DbSet<SubscriptionDB> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccountDB>()
          .HasOne(a => a.Customer)
          .WithMany(c => c.Accounts)
          .HasForeignKey(a => a.CustomerId)
          .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SubscriptionDB>()
                .HasOne(l => l.Account)
                .WithMany(a => a.Subscriptions)
                .HasForeignKey(l => l.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
