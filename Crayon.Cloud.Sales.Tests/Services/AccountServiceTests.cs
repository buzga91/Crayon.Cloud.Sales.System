using Crayon.Cloud.Sales.Application.Services;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared;
using Moq;

namespace Crayon.Cloud.Sales.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _accountService = new AccountService(_accountRepositoryMock.Object);
        }
        [Fact]
        public async Task GetAccountsWithSubscriptions_ShouldReturnSuccess_WhenRepositoryReturnsData()
        {
            // Arrange
            var mockAccountEntities = new List<AccountDB>
        {
            new AccountDB
            {
                Id = 1,
                Name = "Account 1",
                CustomerId = 1,
                Subscriptions = new List<SubscriptionDB>
                {
                    new SubscriptionDB { Id = 1, CustomerId = 1, Customer = new CustomerDB(),  SoftwareName = "Software A", AccountId = 1, State = "Active",MaxQuantity = 1000, MinQuantity =1 ,Quantity =1, SoftwareId = 1, ValidTo = DateTime.Now.AddYears(1),Account = new AccountDB()    }
                }
            }
        };

            _accountRepositoryMock
                .Setup(repo => repo.GetAccountsWithSubscriptions())
                .ReturnsAsync(Result<IEnumerable<AccountDB>>.Success(mockAccountEntities));

            // Act
            var result = await _accountService.GetAccountsWithSubscriptions();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Single(result.Value);
            Assert.Equal("Account 1", result.Value.First().Name);
            _accountRepositoryMock.Verify(repo => repo.GetAccountsWithSubscriptions(), Times.Once);
        }

        [Fact]
        public async Task GetAccountsWithSubscriptions_ShouldReturnFailure_WhenRepositoryFails()
        {
            // Arrange
            const string errorMessage = "Repository failed.";
            _accountRepositoryMock
                .Setup(repo => repo.GetAccountsWithSubscriptions())
                .ReturnsAsync(Result<IEnumerable<AccountDB>>.Failure(errorMessage));

            // Act
            var result = await _accountService.GetAccountsWithSubscriptions();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Error);
            Assert.Null(result.Value);
            _accountRepositoryMock.Verify(repo => repo.GetAccountsWithSubscriptions(), Times.Once);
        }

        [Fact]
        public async Task GetAccountById_ReturnsAccount_WhenSuccess()
        {
            // Arrange
            var accountId = 1;
            var accountEntity = new AccountDB { Id = accountId, Name = "Test Account" };
            _accountRepositoryMock
                .Setup(repo => repo.GetAccountById(accountId))
                .ReturnsAsync(Result<AccountDB>.Success(accountEntity));

            // Act
            var result = await _accountService.GetAccountById(accountId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(accountId, result.Value.Id);
            _accountRepositoryMock.Verify(repo => repo.GetAccountById(accountId), Times.Once);
        }

        [Fact]
        public async Task GetAccountById_ReturnsFailure_WhenRepositoryFails()
        {
            // Arrange
            var accountId = 1;
            var errorMessage = "Account not found";
            _accountRepositoryMock
                .Setup(repo => repo.GetAccountById(accountId))
                .ReturnsAsync(Result<AccountDB>.Failure(errorMessage));

            // Act
            var result = await _accountService.GetAccountById(accountId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Error);
            _accountRepositoryMock.Verify(repo => repo.GetAccountById(accountId), Times.Once);
        }

        [Fact]
        public async Task GetAccounts_ReturnsAccounts_WhenSuccess()
        {
            // Arrange
            var customerId = 123;
            var accountEntities = new List<AccountDB>
            {
                new AccountDB { Id = 1, Name = "Account 1" , CustomerId = 1, Subscriptions = new List<SubscriptionDB>(), Customer = new CustomerDB() },
                new AccountDB { Id = 2, Name = "Account 2", CustomerId = 2, Subscriptions = new List<SubscriptionDB>(), Customer = new CustomerDB() }
            };

            _accountRepositoryMock
                .Setup(repo => repo.GetAccounts(customerId))
                .ReturnsAsync(Result<IEnumerable<AccountDB>>.Success(accountEntities));

            // Act
            var result = await _accountService.GetAccounts(customerId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count());
            _accountRepositoryMock.Verify(repo => repo.GetAccounts(customerId), Times.Once);
        }

        [Fact]
        public async Task GetAccounts_ReturnsFailure_WhenRepositoryFails()
        {
            // Arrange
            var customerId = 123;
            var errorMessage = "Customer not found";
            _accountRepositoryMock
                .Setup(repo => repo.GetAccounts(customerId))
                .ReturnsAsync(Result<IEnumerable<AccountDB>>.Failure(errorMessage));

            // Act
            var result = await _accountService.GetAccounts(customerId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Error);
            _accountRepositoryMock.Verify(repo => repo.GetAccounts(customerId), Times.Once);
        }

    }
}
