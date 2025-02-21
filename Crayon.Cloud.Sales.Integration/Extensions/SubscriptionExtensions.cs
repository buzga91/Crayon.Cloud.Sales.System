using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Domain.Extensions
{
    public static class SubscriptionExtensions
    {

        public static Models.Subscription ToDomain(this SubscriptionDB entity)
        {
            var result = Enum.TryParse<licenseState>(entity.State, out licenseState state);
            if (!result) throw new ArithmeticException($"{nameof(entity.State)} is invalid value");

            return new Models.Subscription
            {
                AccountId = entity.AccountId,
                Id = entity.Id,
                Quantity = entity.Quantity,
                MinQuantity = entity.MinQuantity,
                MaxQuantity = entity.MaxQuantity,
                SoftwareId = entity.SoftwareId,
                SoftwareName = entity.SoftwareName,
                ValidTo = entity.ValidTo,
                State = state
            };
        }

        public static Models.Subscription ToDomain(this ProvisionSubscriptionDTO dto)
        {
            var result = Enum.TryParse<licenseState>(dto.State, out licenseState state);
            if (!result) throw new ArithmeticException($"{nameof(dto.State)} is invalid value. State can be: Active, Canceled ");

            return new Models.Subscription
            {
                AccountId = dto.AccountId,
                SoftwareId = dto.SoftwareId,
                Quantity = dto.Quantity,
                SoftwareName = dto.SoftwareName,
                ValidTo = dto.ValidTo,
                State = state,
            };
        }

        public static SubscriptionDB ToEntity(this Models.Subscription fromDomain, AccountDB accountEntity)
        {

            return new SubscriptionDB
            {
                AccountId = fromDomain.AccountId,
                Id = fromDomain.Id,
                Quantity = fromDomain.Quantity,
                SoftwareName = fromDomain.SoftwareName,
                SoftwareId = fromDomain.SoftwareId,
                ValidTo = fromDomain.ValidTo,
                State = fromDomain.State.ToString(),
                Account = accountEntity,
                MaxQuantity = fromDomain.MaxQuantity,
                MinQuantity = fromDomain.MinQuantity
            };
        }
        public static IEnumerable<Models.Subscription> ToDomainCollection(this IEnumerable<SubscriptionDB> entities)
        {
            var domainLicenses = new List<Models.Subscription>();
            foreach (var entity in entities)
            {
                var result = Enum.TryParse<licenseState>(entity.State, out licenseState state);
                if (!result) throw new ArithmeticException($"{nameof(entity.State)} is invalid value");

                domainLicenses.Add(new Models.Subscription
                {
                    AccountId = entity.AccountId,
                    Id = entity.Id,
                    Quantity = entity.Quantity,
                    MinQuantity = entity.MinQuantity,
                    MaxQuantity = entity.MaxQuantity,
                    SoftwareName = entity.SoftwareName,
                    SoftwareId = entity.SoftwareId,
                    ValidTo = entity.ValidTo,
                    State = state
                });
            }
            return domainLicenses;
        }
    }
}
