using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Application.Contracts
{
    public interface ILicenseService
    {
        Task<Result<IEnumerable<AvailableSoftwareDTO>>> GetAvailableSoftwaresFromCCP();
        Task<Result<License>> ProvisionLicense(License license);
        Task<Result<IEnumerable<License>>> GetLicensesForSpecificAccount(int accountId);
        Task<Result> CancelLicense(int licenseId);
        Task<Result> ExtendLicense(int licenseId, DateTime newValidTo);
    }
}
