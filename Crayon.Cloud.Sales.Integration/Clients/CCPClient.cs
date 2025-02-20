using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;
using Moq;
using System.Text;
using System.Text.Json;

namespace Crayon.Cloud.Sales.Integration.Clients
{
    public class CCPClient : ICCPClient
    {
        private readonly string _baseAddress;
        private readonly Mock<HttpClient> _httpClient;
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
            };
        public CCPClient(string baseAddress, Mock<HttpClient> httpClient)
        {
            _baseAddress = baseAddress;
            _httpClient = httpClient;
            SetupHttpClient();
        }

        private void SetupHttpClient()
        {
            var getAsyncJsonResponse = JsonSerializer.Serialize(availableSoftwares);
            _httpClient.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(getAsyncJsonResponse, Encoding.UTF8, "application/json") }));

        }
        public async Task<Result<IEnumerable<AvailableSoftwareDTO>>> GetAvailableSoftwareServices()
        {
            var response = await _httpClient.Object.GetAsync(_baseAddress);
            var softwares = JsonSerializer.Deserialize<List<AvailableSoftwareDTO>>(response.Content.ToString());
            return Result<IEnumerable<AvailableSoftwareDTO>>.Success(softwares);
        }

        public async Task<Result<PurchasedSoftwareDTO>> ProvisionNewLicense(PurchasedSoftwareDTO softwareService)
        {

            if (availableSoftwares.Any(x => x.Name == softwareService.Name))
            {
                var jsonResponse = JsonSerializer.Serialize(softwareService);
                var content = new StringContent(jsonResponse, Encoding.UTF8, "application/json");
                _httpClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).Returns(Task.FromResult(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json") }));

                var response = await _httpClient.Object.PostAsync(_baseAddress, content);
                var purchasedSoftware = JsonSerializer.Deserialize<PurchasedSoftwareDTO>(response.Content.ToString());
                return Result<PurchasedSoftwareDTO>.Success(purchasedSoftware);
            }
            return Result<PurchasedSoftwareDTO>.Failure("Provisioning failure. Please try to provision one of the available softwares.");
        }
    }
}
