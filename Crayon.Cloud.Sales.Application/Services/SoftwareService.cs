using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Application.Services
{
    public class SoftwareService : ISoftwareService
    {
        private readonly ICCPClient ccpClient;

        public SoftwareService(ICCPClient ccpClient, ISoftwareServiceRepository softwareServiceRepository)
        {
            this.ccpClient = ccpClient;
        }

        public async Task<Result<IEnumerable<AvailableSoftwareDTO>>> GetAvailableSoftwareServices()
        {
            return await ccpClient.GetAvailableSoftwareServices();
        }
        public async Task<Result<PurchasedSoftwareDTO>> ProvisionNewLicense(PurchasedSoftwareDTO softwareService)
        {
            return await ccpClient.ProvisionNewLicense(softwareService);
        }
    }
}
