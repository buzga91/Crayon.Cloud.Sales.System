using Crayon.Cloud.Sales.Domain.Extensions;
using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.Extensions
{
    public static class AccountExtensions
    {
        public static IEnumerable<Account> ToDomainCollection(this IEnumerable<AccountDB> entities)
        {
            var domainAccounts = new List<Account>();
            foreach (var entity in entities)
            {
                domainAccounts.Add(new Account
                {
                    Id = entity.Id,
                    CustomerId = entity.CustomerId,
                    Name = entity.Name,
                    PurchasedSoftwareLicenses = SubscriptionExtensions.ToDomainCollection(entity.Subscriptions)
                });
            }
            return domainAccounts;
        }

        public static Account ToDomain(this AccountDB entity)
        {
            return new Account
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                Name = entity.Name,
                PurchasedSoftwareLicenses = SubscriptionExtensions.ToDomainCollection(entity.Subscriptions)
            };
        }
    }
}
