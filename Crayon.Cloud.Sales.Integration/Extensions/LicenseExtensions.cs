using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Domain.Extensions
{
    public static class LicenseExtensions
    {

        public static Models.License ToDomain(this LicenseDB entity)
        {
            var result = Enum.TryParse<licenseState>(entity.State, out licenseState state);
            if (!result) throw new ArithmeticException($"{nameof(entity.State)} is invalid value");

            return new Models.License
            {
                AccountId = entity.AccountId,
                Id = entity.Id,
                Quantity = entity.Quantity,
                SoftwareName = entity.SoftwareName,
                ValidTo = entity.ValidTo,
                State = state
            };
        }

        public static Models.License ToDomain(this ProvisionLicenseDTO dto)
        {
            var result = Enum.TryParse<licenseState>(dto.State, out licenseState state);
            if (!result) throw new ArithmeticException($"{nameof(dto.State)} is invalid value. State can be: Active, Canceled ");

            return new Models.License
            {
                AccountId = dto.AccountId,
                Id = dto.Id,
                Quantity = dto.Quantity,
                SoftwareName = dto.SoftwareName,
                ValidTo = dto.ValidTo,
                State = state,
            };
        }

        public static LicenseDB ToEntity(this Models.License fromDomain, AccountDB accountEntity)
        {

            return new LicenseDB
            {
                AccountId = fromDomain.AccountId,
                Id = fromDomain.Id,
                Quantity = fromDomain.Quantity,
                SoftwareName = fromDomain.SoftwareName,
                ValidTo = fromDomain.ValidTo,
                State = fromDomain.State.ToString(),
                Account = accountEntity
            };
        }

        //public static ICollection<LicenseDB> ToEntities(this IEnumerable<Models.License> domainCollection, AccountDB accountEntity)
        //{
        //    var entities = new List<LicenseDB>();

        //    foreach (var domain in domainCollection)
        //    {
        //        entities.Add(new LicenseDB
        //        {
        //            Id = domain.Id,
        //            Account = accountEntity,
        //            AccountId = domain.AccountId,
        //            Quantity = domain.Quantity,
        //            SoftwareName = domain.SoftwareName,
        //            ValidTo = domain.ValidTo,
        //            State = domain.State.ToString()
        //        });
        //    }

        //    return entities;
        //}

        public static IEnumerable<Models.License> ToDomainCollection(this IEnumerable<LicenseDB> entities)
        {
            var domainLicenses = new List<Models.License>();
            foreach (var entity in entities)
            {
                var result = Enum.TryParse<licenseState>(entity.State, out licenseState state);
                if (!result) throw new ArithmeticException($"{nameof(entity.State)} is invalid value");

                domainLicenses.Add(new Models.License
                {
                    AccountId = entity.AccountId,
                    Id = entity.Id,
                    Quantity = entity.Quantity,
                    SoftwareName = entity.SoftwareName,
                    ValidTo = entity.ValidTo,
                    State = state
                });
            }
            return domainLicenses;
        }
    }
}
