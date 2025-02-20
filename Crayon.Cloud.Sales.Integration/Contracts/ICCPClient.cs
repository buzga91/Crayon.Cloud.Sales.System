using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Integration.Contracts
{
    public interface ICCPClient
    {
        Task<Result<IEnumerable<AvailableSoftwareDTO>>> GetAvailableSoftwareServices();
        Task<Result<PurchasedSoftwareDTO>> ProvisionNewLicense(PurchasedSoftwareDTO softwareService);
    }
}
