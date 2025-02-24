using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;
using Crayon.Cloud.Sales.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Crayon.Cloud.Sales.Tests.Controllers
{
    public class AccountsControllerTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly AccountsController _controller;

        public AccountsControllerTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new AccountsController(_mockAccountService.Object);
        }

        [Fact]
        public async Task GetAccountsWithPurchasedSubscriptions_ReturnsOkResult_WithAccounts()
        {
            // Arrange
            var accounts = new List<AccountWithPurchasedSubscriptionsDTO>() { new AccountWithPurchasedSubscriptionsDTO { CustomerId = 1, Id = 1, Name = "Test account 1", PurchasedSoftwareLicenses = new List<SubscriptionDTO>() } };
             _mockAccountService.Setup(s => s.GetAccountsWithSubscriptions())
                .ReturnsAsync(Result<IEnumerable<AccountWithPurchasedSubscriptionsDTO>>.Success(accounts));

            // Act
            var result = await _controller.GetAccountsWithPurchasedSubscriptions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var accountsResult = Assert.IsType<Result<IEnumerable<AccountWithPurchasedSubscriptionsDTO>>>(okResult.Value);
            Assert.Equal(accounts, accountsResult.Value);
        }

        [Fact]
        public async Task GetAccountsWithPurchasedSubscriptions_ReturnsBadRequest_OnException()
        {
            // Arrange
            _mockAccountService.Setup(s => s.GetAccountsWithSubscriptions())
                .ThrowsAsync(new Exception("Error fetching accounts"));

            // Act
            var result = await _controller.GetAccountsWithPurchasedSubscriptions();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error fetching accounts", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAccountsByCustomerId_ReturnsOkResult_WithAccounts()
        {
            // Arrange
            var customerId = 1;
            var accounts = new List<AccountDTO>() { new AccountDTO { CustomerId = 1, Id = 1, Name = "Test account 1"} };
            _mockAccountService.Setup(s => s.GetAccounts(customerId))
                .ReturnsAsync(Result<IEnumerable<AccountDTO>>.Success(accounts));
            
            // Act
            var result = await _controller.GetAccountsByCustomerId(customerId);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var accountResult = Assert.IsType<Result<IEnumerable<AccountDTO>>>(okResult.Value);
            Assert.Equal(accounts, accountResult.Value);
        }

        [Fact]
        public async Task GetAccountsByCustomerId_ReturnsBadRequest_OnException()
        {
            // Arrange
            var customerId = 1;
            _mockAccountService.Setup(s => s.GetAccounts(customerId))
                .ThrowsAsync(new Exception("Error fetching accounts for customer"));

            // Act
            var result = await _controller.GetAccountsByCustomerId(customerId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error fetching accounts for customer", badRequestResult.Value);
        }
    }
}
