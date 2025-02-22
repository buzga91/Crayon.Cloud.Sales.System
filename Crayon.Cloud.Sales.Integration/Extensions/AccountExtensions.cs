using Crayon.Cloud.Sales.Domain.Extensions;
using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared.DTO;

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

        public static IEnumerable<AccountDTO> ToDtoCollection(this IEnumerable<AccountDB> entities)
        {
            var dtoAccounts = new List<AccountDTO>();
            foreach (var entity in entities)
            {
                dtoAccounts.Add(new AccountDTO
                {
                    Id = entity.Id,
                    CustomerId = entity.CustomerId,
                    Name = entity.Name
                });
            }
            return dtoAccounts;
        }

        public static IEnumerable<AccountWithPurchasedSubscriptionsDTO> ToDtoCollectionWithPurchasedSubscriptions(this IEnumerable<AccountDB> entities)
        {
            var dtoAccounts = new List<AccountWithPurchasedSubscriptionsDTO>();
            foreach (var entity in entities)
            {
                dtoAccounts.Add(new AccountWithPurchasedSubscriptionsDTO
                {
                    Id = entity.Id,
                    CustomerId = entity.CustomerId,
                    Name = entity.Name,
                    PurchasedSoftwareLicenses = SubscriptionExtensions.ToDtoCollection(entity.Subscriptions)
                });
            }
            return dtoAccounts;
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
        public static AccountDTO ToDto(this AccountDB entity)
        {
            return new AccountDTO
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                Name = entity.Name
            };
        }
    }
}
