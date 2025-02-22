using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Extensions;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Integration.Clients
{
    public class CCPClient : ICCPClient
    {
        private List<AvailableSoftwareDTO> availableSoftwares = new List<AvailableSoftwareDTO>()
            {
                new AvailableSoftwareDTO()
                {
                    Id = 1,
                    Name = "Microsoft Office",
                    MinQuantity = 1,
                    MaxQuantity = 1000,
                    Description = "Microsoft Office Software"
                },
                new AvailableSoftwareDTO()
                {
                    Id = 2,
                    Name = "Microsoft Visual Studio",
                   MinQuantity = 1,
                    MaxQuantity = 5000,
                    Description = "Microsoft Visual Studio Software"
                },
                new AvailableSoftwareDTO()
                {
                    Id = 3,
                     Name = "Microsoft E1",
                     MinQuantity = 1,
                    MaxQuantity = 100,
                    Description = "Microsoft E1 Software"
                },
                 new AvailableSoftwareDTO()
                {
                    Id = 4,
                     Name = "Microsoft E2",
                     MinQuantity = 1,
                    MaxQuantity = 10000,
                    Description = "Microsoft E2 Software"
                },
                   new AvailableSoftwareDTO()
                {
                    Id = 5,
                     Name = "Microsoft Teams",
                     MinQuantity = 1,
                    MaxQuantity = 2000,
                    Description = "Microsoft Teams Software"
                },
                     new AvailableSoftwareDTO()
                {
                    Id = 6,
                     Name = "Microsoft E3",
                     MinQuantity = 1,
                    MaxQuantity = 3000,
                    Description = "Microsoft E3 Software"
                },
                       new AvailableSoftwareDTO()
                {
                    Id = 7,
                     Name = "Microsoft E4",
                     MinQuantity = 1,
                    MaxQuantity = 4000,
                    Description = "Microsoft E4 Software"
                },
                         new AvailableSoftwareDTO()
                {
                    Id = 8,
                     Name = "Microsoft E5",
                     MinQuantity = 1,
                    MaxQuantity = 5000,
                    Description = "Microsoft E5 Software"
                },
            };

        public async Task<Result<AvailableSoftwareDTO>> GetAvailableSoftwareServicesById(int softwareId)
        {
            var availableSoftwareService = availableSoftwares.FirstOrDefault(x => x.Id == softwareId);
            if (availableSoftwareService is null)
            {
                string message = "Software doesn't exist, or it is not available, please try with available software Id";
                Console.WriteLine(message);
                return Result<AvailableSoftwareDTO>.Failure(message);
            }
            return Result<AvailableSoftwareDTO>.Success(availableSoftwareService);
        }
        public async Task<Result<IEnumerable<AvailableSoftwareDTO>>> GetAvailableSoftwareServices()
        {
            return Result<IEnumerable<AvailableSoftwareDTO>>.Success(availableSoftwares);
        }

        public async Task<Result<PurchasedSoftwareDTO>> ProvisionSoftware(ProvisionSoftwareDTO provisionSoftwareDTO)
        {
            var availableSoftware = await GetAvailableSoftwareServicesById(provisionSoftwareDTO.Id);
            if (!availableSoftware.IsSuccess)
            {
                Console.WriteLine(availableSoftware.Error);
                return Result<PurchasedSoftwareDTO>.Failure(availableSoftware.Error);
            }
            else if (provisionSoftwareDTO.Quantity > availableSoftware.Value.MaxQuantity
                || provisionSoftwareDTO.Quantity < availableSoftware.Value.MinQuantity)
            {
                string message = "Provisioning failure. License quantity can't be more then maximum license value, or less then minimum license quantity value. Check the values from available softwares!";
                Console.WriteLine(message);
                return Result<PurchasedSoftwareDTO>.Failure(message);
            }
            var purchasedSoftwareDto = SoftwareExtensions.ToCcpPurchasedDto(provisionSoftwareDTO, availableSoftware.Value.Name);
            return Result<PurchasedSoftwareDTO>.Success(purchasedSoftwareDto);
        }
    }
}
