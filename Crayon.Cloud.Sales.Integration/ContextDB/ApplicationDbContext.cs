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

            // Configure Account to Subscription relationship
            modelBuilder.Entity<SubscriptionDB>()
                .HasOne(s => s.Account) // One Subscription has one Account
                .WithMany(a => a.Subscriptions) // One Account has many Subscriptions
                .HasForeignKey(s => s.AccountId) // FK in Subscription
                .OnDelete(DeleteBehavior.Cascade); // Cascade on delete

            // Configure Customer to Subscription relationship
            modelBuilder.Entity<SubscriptionDB>()
                .HasOne(s => s.Customer) // One Subscription has one Customer
                .WithMany() // No navigation property in CustomerDB
                .HasForeignKey(s => s.CustomerId) // FK in Subscription
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade on delete
        }
    }
}
