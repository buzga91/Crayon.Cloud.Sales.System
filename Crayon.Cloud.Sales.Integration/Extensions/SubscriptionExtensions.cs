using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Integration.Extensions
{
    public static class SubscriptionExtensions
    {

        public static Subscription ToDomain(this SubscriptionDB entity)
        {
            var result = Enum.TryParse<licenseState>(entity.State, out licenseState state);
            if (!result) throw new ArithmeticException($"{nameof(entity.State)} is invalid value");

            return new Subscription
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

        public static Subscription ToDomain(this ProvisionSubscriptionDTO dto)
        {
            var result = Enum.TryParse<licenseState>(dto.State, out licenseState state);
            if (!result) throw new ArithmeticException($"{nameof(dto.State)} is invalid value. State can be: Active, Canceled ");

            return new Subscription
            {
                CustomerId = dto.CustomerId,
                AccountId = dto.AccountId,
                SoftwareId = dto.SoftwareId,
                Quantity = dto.Quantity,
                SoftwareName = dto.SoftwareName,
                ValidTo = dto.ValidTo,
                State = state,
            };
        }

        public static SubscriptionDB ToEntity(this Subscription fromDomain, AccountDB accountEntity)
        {

            return new SubscriptionDB
            {
                Id = fromDomain.Id,
                AccountId = fromDomain.AccountId,
              CustomerId = fromDomain.CustomerId,
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
        public static IEnumerable<Subscription> ToDomainCollection(this IEnumerable<SubscriptionDB> entities)
        {
            var domainLicenses = new List<Subscription>();
            foreach (var entity in entities)
            {
                var result = Enum.TryParse<licenseState>(entity.State, out licenseState state);
                if (!result) throw new ArithmeticException($"{nameof(entity.State)} is invalid value");

                domainLicenses.Add(new Subscription
                {
                    Id = entity.Id,
                    AccountId = entity.AccountId,
                    CustomerId = entity.CustomerId,
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

        public static IEnumerable<SubscriptionDTO> ToDtoCollection(this IEnumerable<SubscriptionDB> entities)
        {
            var domainLicenses = new List<SubscriptionDTO>();
            foreach (var entity in entities)
            {
                var result = Enum.TryParse<licenseState>(entity.State, out licenseState state);
                if (!result) throw new ArithmeticException($"{nameof(entity.State)} is invalid value");

                domainLicenses.Add(new SubscriptionDTO
                {
                    Id = entity.Id,
                    AccountId = entity.AccountId,
                    CustomerId = entity.CustomerId,
                    Quantity = entity.Quantity,
                    MinQuantity = entity.MinQuantity,
                    MaxQuantity = entity.MaxQuantity,
                    SoftwareName = entity.SoftwareName,
                    SoftwareId = entity.SoftwareId,
                    ValidTo = entity.ValidTo,
                    State = state.ToString()
                });
            }
            return domainLicenses;
        }

        public static SubscriptionDTO ToDto(this SubscriptionDB entity)
        {
            var result = Enum.TryParse<licenseState>(entity.State, out licenseState state);
            if (!result) throw new ArithmeticException($"{nameof(entity.State)} is invalid value");

            return new SubscriptionDTO
            {
                Id = entity.Id,
                AccountId = entity.AccountId,
                CustomerId = entity.CustomerId,
                Quantity = entity.Quantity,
                MinQuantity = entity.MinQuantity,
                MaxQuantity = entity.MaxQuantity,
                SoftwareId = entity.SoftwareId,
                SoftwareName = entity.SoftwareName,
                ValidTo = entity.ValidTo,
                State = state.ToString()
            };
        }
    }
}
