using Crayon.Cloud.Sales.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Crayon.Cloud.Sales.Integration.Clients
{
    public class CCPClient
    {
        private readonly string _baseAddress;
        private readonly Mock<HttpClient> _httpClient;
        public CCPClient(string baseAddress, Mock<HttpClient> httpClient)
        {
            _baseAddress = baseAddress;
            _httpClient = httpClient;
        }

        private void SetupHttpClient()
        {
            var softwareServices = new List<SoftwareService>()
            {
                new SoftwareService()
                {
                    Description ="Test Description 2",
                    Id = 2,
                    Name = "Test Software 2",
                    PricePerLicense = 20
                },
                new SoftwareService()
                {
                    Description ="Test Description 3",
                    Id = 3,
                    Name = "Test Software 3",
                    PricePerLicense = 30
                },
                new SoftwareService()
                {
                    Description ="Test Description 1",
                    Id = 1,
                    Name = "Test Software 1",
                    PricePerLicense = 10
                },
            };
            var jsonResponse = JsonSerializer.Serialize(softwareServices);

            _httpClient.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult( new HttpResponseMessage {StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")}));
        }

        public async Task<IEnumerable<SoftwareService>> GetAvailableSoftwareServices()
        {
            var response = await _httpClient.Object.GetAsync(_baseAddress);
            return JsonSerializer.Deserialize<List<SoftwareService>>(response.Content.ToString());
        }
    }
}
