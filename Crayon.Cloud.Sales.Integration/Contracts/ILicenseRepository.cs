using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.Contracts
{
    public interface ILicenseRepository
    {
        Task<LicenseDB> Add(LicenseDB license);
        Task<IEnumerable<LicenseDB>> GetLicensesByAccountId(int accountId);
        Task<LicenseDB> GetLicensesById(int licenseId);
        Task Update(LicenseDB license);
    }
}
