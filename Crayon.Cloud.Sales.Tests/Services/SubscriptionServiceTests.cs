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
        public async Task ChangeSubscriptionQuantity_ShouldReturnSuccess_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var subscriptionId = 1;
            var newQuantity = 5;

            _mockSubscriptionRepo
                .Setup(repo => repo.ChangeSubscriptionQuantity(subscriptionId, newQuantity))
                .ReturnsAsync(Result.Success());

            // Act
            var result = await _subscriptionService.ChangeSubscriptionQuantity(subscriptionId, newQuantity);

            // Assert
            Assert.True(result.IsSuccess);
            _mockSubscriptionRepo.Verify(repo => repo.ChangeSubscriptionQuantity(subscriptionId, newQuantity), Times.Once);
        }

        [Fact]
        public async Task ChangeSubscriptionQuantity_ShouldReturnFailure_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var subscriptionId = 1;
            var newQuantity = 5;
            var errorMessage = "Failed to change subscription quantity.";

            _mockSubscriptionRepo
                .Setup(repo => repo.ChangeSubscriptionQuantity(subscriptionId, newQuantity))
                .ReturnsAsync(Result.Failure(errorMessage));

            // Act
            var result = await _subscriptionService.ChangeSubscriptionQuantity(subscriptionId, newQuantity);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Error);
            _mockSubscriptionRepo.Verify(repo => repo.ChangeSubscriptionQuantity(subscriptionId, newQuantity), Times.Once);
        }

        [Fact]
        public async Task GetSubscriptionsForSpecificAccount_ShouldReturnSubscriptions_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var accountId = 101;
            var subscriptions = new List<SubscriptionDB>
        {
            new SubscriptionDB { Id = 1, AccountId = accountId, SoftwareId = 2, Quantity = 3, ValidTo = DateTime.Now.AddMonths(1), MaxQuantity = 1000, MinQuantity = 1,State="Active", SoftwareName = "Test software name 1", Account = new AccountDB() },
            new SubscriptionDB { Id = 2, AccountId = accountId, SoftwareId = 3, Quantity = 5 ,ValidTo = DateTime.Now.AddMonths(1), MaxQuantity = 2000, MinQuantity = 1,State="Active", SoftwareName = "Test software name 2", Account = new AccountDB()}
        };

            _mockSubscriptionRepo
                .Setup(repo => repo.GetSubscriptionsByAccountId(accountId))
                .ReturnsAsync(Result<IEnumerable<SubscriptionDB>>.Success(subscriptions));

            // Act
            var result = await _subscriptionService.GetSubscriptionsForSpecificAccount(accountId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count());
            _mockSubscriptionRepo.Verify(repo => repo.GetSubscriptionsByAccountId(accountId), Times.Once);
        }

        [Fact]
        public async Task GetSubscriptionsForSpecificAccount_ShouldReturnFailure_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var accountId = 101;
            var errorMessage = "Failed to retrieve subscriptions.";

            _mockSubscriptionRepo
                .Setup(repo => repo.GetSubscriptionsByAccountId(accountId))
                .ReturnsAsync(Result<IEnumerable<SubscriptionDB>>.Failure(errorMessage));

            // Act
            var result = await _subscriptionService.GetSubscriptionsForSpecificAccount(accountId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Error);
            _mockSubscriptionRepo.Verify(repo => repo.GetSubscriptionsByAccountId(accountId), Times.Once);
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
        public async Task ExtendSubscriptionValidDate_ShouldReturnFailure_WhenNewValidToDateIsNotGreaterThenCurrentValidToDate()
        {
            // Arrange
            var subscriptionId = 1;
            var newValidTo = DateTime.Now.AddYears(-1);
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
