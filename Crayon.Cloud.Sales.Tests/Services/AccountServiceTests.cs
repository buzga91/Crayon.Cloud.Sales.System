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
                new AccountDB { Id = 1, Name = "Account 1" },
                new AccountDB { Id = 2, Name = "Account 2" }
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
