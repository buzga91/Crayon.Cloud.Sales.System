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
                .HasOne(s => s.Account) 
                .WithMany(a => a.Subscriptions) 
                .HasForeignKey(s => s.AccountId) 
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<SubscriptionDB>()
                .HasOne(s => s.Customer) 
                .WithMany() 
                .HasForeignKey(s => s.CustomerId) 
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
