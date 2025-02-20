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
                    PurchasedSoftwareLicenses = LicenseExtensions.ToDomainCollection(entity.Licenses)
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
                PurchasedSoftwareLicenses = LicenseExtensions.ToDomainCollection(entity.Licenses)
            };
        }

        //public static AccountDB ToEntity(this Account domainAccount, CustomerDB customerEntity)
        //{
        //    var accountDB = new AccountDB
        //    {
        //        Id = domainAccount.Id,
        //        CustomerId = domainAccount.CustomerId,
        //        Name = domainAccount.Name,
        //        Customer = customerEntity,
        //    };
        //    accountDB.Licenses = LicenseExtensions.ToEntities(domainAccount.PurchasedSoftwareLicenses, accountDB);
        //    return accountDB;
        //}
    }
}
