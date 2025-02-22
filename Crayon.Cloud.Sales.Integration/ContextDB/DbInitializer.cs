using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.ContextDB
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;
            }

            var customers = new CustomerDB[]
            {
            new CustomerDB { Name = "Customer A", Id  = 1 },
            new CustomerDB { Name = "Customer B", Id  = 2 },
            new CustomerDB { Name = "Customer C", Id  = 3 }
        };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            var addedCustomers = context.Customers.ToList();

            var accounts = new AccountDB[]
            {
            new AccountDB { Name = "Account 1", CustomerId = addedCustomers[0].Id, Customer = addedCustomers[0] },
            new AccountDB { Name = "Account 2", CustomerId = addedCustomers[1].Id, Customer = addedCustomers[1] },
            new AccountDB { Name = "Account 3", CustomerId = addedCustomers[1].Id, Customer = addedCustomers[1] }
        };

            context.Accounts.AddRange(accounts);
            context.SaveChanges();

            var addedAccounts = context.Accounts.ToList();

            var Subscriptions = new SubscriptionDB[]
            {
            new SubscriptionDB { SoftwareName = "Microsoft Office", Quantity = 10, State = "Active", ValidTo = DateTime.UtcNow.AddYears(1), AccountId = addedAccounts[0].Id, MinQuantity = 1, MaxQuantity = 1000, SoftwareId = 1 },
            new SubscriptionDB { SoftwareName = "Microsoft Visual Studio", Quantity = 5, State = "Active", ValidTo = DateTime.UtcNow.AddYears(1), AccountId = addedAccounts[0].Id,MinQuantity = 1, MaxQuantity = 5000, SoftwareId = 2 },
             new SubscriptionDB { SoftwareName = "Microsoft E1", Quantity = 7, State = "Active", ValidTo = DateTime.UtcNow.AddYears(1), AccountId = addedAccounts[1].Id,MinQuantity = 1, MaxQuantity = 100, SoftwareId = 3 }
        };

            context.Subscriptions.AddRange(Subscriptions);
            context.SaveChanges();
            Console.WriteLine("Database migration and seeding completed successfully.");
        }
    }
}
