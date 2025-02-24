using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Application.Services
{
    public class SoftwareService : ISoftwareService
    {
        private readonly ICCPClient ccpClient;

        public SoftwareService(ICCPClient ccpClient)
        {
            this.ccpClient = ccpClient;
        }

        public async Task<Result<IEnumerable<AvailableSoftwareDTO>>> GetAvailableSoftwareServices()
        {
            var result = await ccpClient.GetAvailableSoftwareServices();
            if (!result.IsSuccess) return Result<IEnumerable<AvailableSoftwareDTO>>.Failure(result.Error);
            return result;
        }

        public async Task<Result<AvailableSoftwareDTO>> GetAvailableSoftwareServicesById(int softwareId)
        {
            var result = await ccpClient.GetAvailableSoftwareServicesById(softwareId);
            if (!result.IsSuccess) return Result<AvailableSoftwareDTO>.Failure(result.Error);
            return result;
        }

        public async Task<Result<PurchasedSoftwareDTO>> ProvisionSoftware(ProvisionSoftwareDTO provisionSoftwareDTO)
        {
            var result = await ccpClient.ProvisionSoftware(provisionSoftwareDTO);
            if (!result.IsSuccess) return Result<PurchasedSoftwareDTO>.Failure(result.Error);
            return result;
        }
    }
}
