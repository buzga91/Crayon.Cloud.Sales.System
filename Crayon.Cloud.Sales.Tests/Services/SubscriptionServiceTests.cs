using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Application.Services;
using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Shared.DTO;
using Crayon.Cloud.Sales.Shared;
using Moq;
using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Tests.Services
{
    public class SubscriptionServiceTests
    {
        private readonly Mock<ISubscriptionRepository> _mockSubscriptionRepo;
        private readonly Mock<IAccountRepository> _mockAccountRepo;
        private readonly Mock<ICustomerRepository> _mockCustomerRepo;
        private readonly Mock<ISoftwareService> _mockSoftwareService;
        private readonly SubscriptionService _subscriptionService;

        public SubscriptionServiceTests()
        {
            _mockSubscriptionRepo = new Mock<ISubscriptionRepository>();
            _mockAccountRepo = new Mock<IAccountRepository>();
            _mockCustomerRepo = new Mock<ICustomerRepository>();
            _mockSoftwareService = new Mock<ISoftwareService>();
            _subscriptionService = new SubscriptionService(
                _mockSubscriptionRepo.Object,
                _mockAccountRepo.Object,
                _mockCustomerRepo.Object,
                _mockSoftwareService.Object
            );
        }

        [Fact]
        public async Task GetAvailableSoftwaresFromCCP_ShouldReturnSuccess_WhenSoftwareServiceIsSuccessful()
        {
            // Arrange
            var mockResult = Result<IEnumerable<AvailableSoftwareDTO>>.Success(new List<AvailableSoftwareDTO>());
            _mockSoftwareService.Setup(s => s.GetAvailableSoftwareServices()).ReturnsAsync(mockResult);

            // Act
            var result = await _subscriptionService.GetAvailableSoftwaresFromCCP();

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetAvailableSoftwaresFromCCP_ShouldReturnFailure_WhenSoftwareServiceFails()
        {
            // Arrange
            var mockResult = Result<IEnumerable<AvailableSoftwareDTO>>.Failure("Error");
            _mockSoftwareService.Setup(s => s.GetAvailableSoftwareServices()).ReturnsAsync(mockResult);

            // Act
            var result = await _subscriptionService.GetAvailableSoftwaresFromCCP();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Error", result.Error);
        }

        [Fact]
        public async Task CancelSubscription_ShouldReturnSuccess_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var subscriptionId = 1;
            var mockResult = Result.Success();
            _mockSubscriptionRepo.Setup(r => r.ChangeSubscriptionState(subscriptionId, "Canceled")).ReturnsAsync(mockResult);

            // Act
            var result = await _subscriptionService.CancelSubscription(subscriptionId);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task CancelSubscription_ShouldReturnFailure_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var subscriptionId = 1;
            var mockResult = Result.Failure("Error");
            _mockSubscriptionRepo.Setup(r => r.ChangeSubscriptionState(subscriptionId, "Canceled")).ReturnsAsync(mockResult);

            // Act
            var result = await _subscriptionService.CancelSubscription(subscriptionId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Error", result.Error);
        }

        [Fact]
        public async Task ExtendSubscriptionValidDate_ShouldReturnSuccess_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var subscriptionId = 1;
            var newValidTo = DateTime.Now.AddMonths(1);
            var mockResult = Result.Success();
            _mockSubscriptionRepo.Setup(r => r.ExtendSubscriptionValidationTime(subscriptionId, newValidTo)).ReturnsAsync(mockResult);

            // Act
            var result = await _subscriptionService.ExtendSubscriptionValidDate(subscriptionId, newValidTo);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ExtendSubscriptionValidDate_ShouldReturnFailure_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var subscriptionId = 1;
            var newValidTo = DateTime.Now.AddMonths(1);
            var mockResult = Result.Failure("Error");
            _mockSubscriptionRepo.Setup(r => r.ExtendSubscriptionValidationTime(subscriptionId, newValidTo)).ReturnsAsync(mockResult);

            // Act
            var result = await _subscriptionService.ExtendSubscriptionValidDate(subscriptionId, newValidTo);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Error", result.Error);
        }

        [Fact]
        public async Task ProvisionSubscription_ShouldReturnSuccess_WhenAllServicesReturnSuccess()
        {
            // Arrange
            var subscription = new Subscription { SoftwareId = 1, AccountId = 1, MaxQuantity = 100, MinQuantity = 1, Quantity = 1, SoftwareName = "Test software name", Id = 1, State = licenseState.Active, ValidTo = DateTime.Now.AddYears(1) };
            var mockSoftwareResult = Result<AvailableSoftwareDTO>.Success(new AvailableSoftwareDTO {Description = "Test Description", Id = 1, MaxQuantity = 1000, MinQuantity = 1, Name = "Test name"  });
            var mockAccountResult = Result<AccountDB>.Success(new AccountDB { Id = 1 ,Customer = new CustomerDB(), CustomerId = 1 , Name = "Test account name", Subscriptions = new List<SubscriptionDB>() });
            var mockCustomerResult = Result<CustomerDB>.Success(new CustomerDB { CustomerCcpId = 1, Accounts = new List<AccountDB>(), Id = 1, Name = "Test customer name" });
            var mockProvisionResult = Result<PurchasedSoftwareDTO>.Success(new PurchasedSoftwareDTO {Name = "Test name", Quantity = 1, State = "Active", ValidTo = DateTime.Now.AddYears(1) });

            _mockSoftwareService.Setup(s => s.GetAvailableSoftwareServicesById(subscription.SoftwareId)).ReturnsAsync(mockSoftwareResult);
            _mockAccountRepo.Setup(r => r.GetAccountById(subscription.AccountId)).ReturnsAsync(mockAccountResult);
            _mockCustomerRepo.Setup(r => r.GetCustomerById(mockAccountResult.Value.Id)).ReturnsAsync(mockCustomerResult);
            _mockSoftwareService.Setup(s => s.ProvisionSoftware(It.IsAny<ProvisionSoftwareDTO>())).ReturnsAsync(mockProvisionResult);

            // Act
            var result = await _subscriptionService.ProvisionSubscription(subscription);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ProvisionSubscription_ShouldReturnFailure_WhenSoftwareServiceReturnsFailure()
        {
            // Arrange
            var subscription = new Subscription { SoftwareId = 1, AccountId = 1, MaxQuantity = 100, MinQuantity = 1, Quantity = 1, SoftwareName = "Test software name", Id = 1, State = licenseState.Active, ValidTo = DateTime.Now.AddYears(1) };
            var mockSoftwareResult = Result<AvailableSoftwareDTO>.Failure("Error");

            _mockSoftwareService.Setup(s => s.GetAvailableSoftwareServicesById(subscription.SoftwareId)).ReturnsAsync(mockSoftwareResult);

            // Act
            var result = await _subscriptionService.ProvisionSubscription(subscription);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Error", result.Error);
        }
    }
}
