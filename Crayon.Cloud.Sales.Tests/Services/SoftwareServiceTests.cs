using Crayon.Cloud.Sales.Application.Services;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Shared.DTO;
using Crayon.Cloud.Sales.Shared;
using Moq;

namespace Crayon.Cloud.Sales.Tests.Services
{
    public class SoftwareServiceTests
    {
        private readonly Mock<ICCPClient> _ccpClientMock;
        private readonly SoftwareService _softwareService;

        public SoftwareServiceTests()
        {
            _ccpClientMock = new Mock<ICCPClient>();
            _softwareService = new SoftwareService(_ccpClientMock.Object);
        }

        [Fact]
        public async Task GetAvailableSoftwareServices_ReturnsServices_WhenSuccess()
        {
            // Arrange
            var softwareList = new List<AvailableSoftwareDTO>
            {
                new AvailableSoftwareDTO { Id = 1, Name = "Software 1" },
                new AvailableSoftwareDTO { Id = 2, Name = "Software 2" }
            };

            _ccpClientMock
                .Setup(client => client.GetAvailableSoftwareServices())
                .ReturnsAsync(Result<IEnumerable<AvailableSoftwareDTO>>.Success(softwareList));

            // Act
            var result = await _softwareService.GetAvailableSoftwareServices();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count());
            _ccpClientMock.Verify(client => client.GetAvailableSoftwareServices(), Times.Once);
        }

        [Fact]
        public async Task GetAvailableSoftwareServices_ReturnsFailure_WhenRepositoryFails()
        {
            // Arrange
            var errorMessage = "Failed to fetch software services";
            _ccpClientMock
                .Setup(client => client.GetAvailableSoftwareServices())
                .ReturnsAsync(Result<IEnumerable<AvailableSoftwareDTO>>.Failure(errorMessage));

            // Act
            var result = await _softwareService.GetAvailableSoftwareServices();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Error);
            _ccpClientMock.Verify(client => client.GetAvailableSoftwareServices(), Times.Once);
        }

        [Fact]
        public async Task GetAvailableSoftwareServicesById_ReturnsService_WhenSuccess()
        {
            // Arrange
            var softwareId = 1;
            var software = new AvailableSoftwareDTO { Id = softwareId, Name = "Software 1" };

            _ccpClientMock
                .Setup(client => client.GetAvailableSoftwareServicesById(softwareId))
                .ReturnsAsync(Result<AvailableSoftwareDTO>.Success(software));

            // Act
            var result = await _softwareService.GetAvailableSoftwareServicesById(softwareId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(softwareId, result.Value.Id);
            _ccpClientMock.Verify(client => client.GetAvailableSoftwareServicesById(softwareId), Times.Once);
        }

        [Fact]
        public async Task GetAvailableSoftwareServicesById_ReturnsFailure_WhenRepositoryFails()
        {
            // Arrange
            var softwareId = 1;
            var errorMessage = "Software not found";

            _ccpClientMock
                .Setup(client => client.GetAvailableSoftwareServicesById(softwareId))
                .ReturnsAsync(Result<AvailableSoftwareDTO>.Failure(errorMessage));

            // Act
            var result = await _softwareService.GetAvailableSoftwareServicesById(softwareId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Error);
            _ccpClientMock.Verify(client => client.GetAvailableSoftwareServicesById(softwareId), Times.Once);
        }

        [Fact]
        public async Task ProvisionSoftware_ReturnsProvisionedSoftware_WhenSuccess()
        {
            // Arrange
            var provisionSoftwareDTO = new ProvisionSoftwareDTO { Id = 1, Quantity = 1 , State = "Active", ValidTo = DateTime.Now.AddYears(1), CustomerCcpId = 1};
            var purchasedSoftware = new PurchasedSoftwareDTO { Quantity = 1, Name = "Software 1", State = "Active", ValidTo = DateTime.Now.AddYears(1) };

            _ccpClientMock
                .Setup(client => client.ProvisionSoftware(provisionSoftwareDTO))
                .ReturnsAsync(Result<PurchasedSoftwareDTO>.Success(purchasedSoftware));

            // Act
            var result = await _softwareService.ProvisionSoftware(provisionSoftwareDTO);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            _ccpClientMock.Verify(client => client.ProvisionSoftware(provisionSoftwareDTO), Times.Once);
        }

        [Fact]
        public async Task ProvisionSoftware_ReturnsFailure_WhenProvisioningFails()
        {
            // Arrange
            var provisionSoftwareDTO = new ProvisionSoftwareDTO { Id = 1, Quantity = 1, State = "Active", ValidTo = DateTime.Now.AddYears(1), CustomerCcpId = 1 };
            var errorMessage = "Provisioning failed";

            _ccpClientMock
                .Setup(client => client.ProvisionSoftware(provisionSoftwareDTO))
                .ReturnsAsync(Result<PurchasedSoftwareDTO>.Failure(errorMessage));

            // Act
            var result = await _softwareService.ProvisionSoftware(provisionSoftwareDTO);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Error);
            _ccpClientMock.Verify(client => client.ProvisionSoftware(provisionSoftwareDTO), Times.Once);
        }
    }
}
