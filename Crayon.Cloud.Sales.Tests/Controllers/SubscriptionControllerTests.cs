using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;
using Crayon.Cloud.Sales.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Crayon.Cloud.Sales.Tests.Controllers
{
    public class SubscriptionControllerTests
    {
        private readonly Mock<ISubscriptionService> _mockSubscriptionService;
        private readonly SubscriptionController _controller;

        public SubscriptionControllerTests()
        {
            _mockSubscriptionService = new Mock<ISubscriptionService>();
            _controller = new SubscriptionController(_mockSubscriptionService.Object);
        }

        [Fact]
        public async Task GetAvailableSoftwares_ReturnsOkResult_WithSoftwares()
        {
            // Arrange
            var softwares = new List<AvailableSoftwareDTO> { new AvailableSoftwareDTO { Description = "Test description", Id = 1, MaxQuantity = 1000, MinQuantity = 1, Name = "Test Name" } };
            _mockSubscriptionService.Setup(s => s.GetAvailableSoftwaresFromCCP())
                .ReturnsAsync(Result<IEnumerable< AvailableSoftwareDTO >>.Success(softwares));

            // Act
            var result = await _controller.GetAvailableSoftwares();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var softwaresResult = Assert.IsType<Result<IEnumerable<AvailableSoftwareDTO>>>(okResult.Value);
            Assert.Equal(softwares, softwaresResult.Value);
        }

        [Fact]
        public async Task GetAvailableSoftwares_ReturnsBadRequest_OnException()
        {
            // Arrange
            var errorMessage = "Error fetching softwares";
            _mockSubscriptionService.Setup(s => s.GetAvailableSoftwaresFromCCP())
                .ThrowsAsync(new Exception(errorMessage));

            // Act
            var result = await _controller.GetAvailableSoftwares();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task CancelSubscription_ReturnsOkResult_OnSuccess()
        {
            // Arrange
            var subscriptionId = 1;
            var expectedResult = true;
            _mockSubscriptionService.Setup(s => s.CancelSubscription(subscriptionId))
                .ReturnsAsync(Result.Success());

            // Act
            var result = await _controller.CancelSubscription(subscriptionId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var cancelSubscriptionResult = Assert.IsType<Result>(okResult.Value);
            Assert.Equal(expectedResult, cancelSubscriptionResult.IsSuccess);
        }

        [Fact]
        public async Task CancelSubscription_ReturnsBadRequest_OnException()
        {
            // Arrange
            var subscriptionId = 1;
            var errorMessage = "Error canceling subscription";
            _mockSubscriptionService.Setup(s => s.CancelSubscription(subscriptionId))
                .ThrowsAsync(new Exception(errorMessage));

            // Act
            var result = await _controller.CancelSubscription(subscriptionId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task ExtendSubscriptionValidDate_ReturnsOkResult_OnSuccess()
        {
            // Arrange
            var dto = new ExtendSubscriptionValidDateDTO { SubscriptionId = 1, NewValidTo = DateTime.UtcNow.AddDays(30) };
            var expectedResult = true;
            _mockSubscriptionService.Setup(s => s.ExtendSubscriptionValidDate(dto.SubscriptionId, dto.NewValidTo))
                .ReturnsAsync(Result.Success());

            // Act
            var result = await _controller.ExtendSubscriptionValidDate(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var extendSubscriptionResult = Assert.IsType<Result>(okResult.Value);
            Assert.Equal(expectedResult, extendSubscriptionResult.IsSuccess);
        }

        [Fact]
        public async Task ExtendSubscriptionValidDate_ReturnsBadRequest_OnException()
        {
            // Arrange
            var dto = new ExtendSubscriptionValidDateDTO { SubscriptionId = 1, NewValidTo = DateTime.UtcNow.AddDays(-3330) };
            var errorMessage = "Error extending subscription";
            _mockSubscriptionService.Setup(s => s.ExtendSubscriptionValidDate(dto.SubscriptionId, dto.NewValidTo))
                .ThrowsAsync(new Exception(errorMessage));

            // Act
            var result = await _controller.ExtendSubscriptionValidDate(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task ChangeSubscriptionQuantity_ReturnsOkResult_OnSuccess()
        {
            // Arrange
            var dto = new ChangeSubscriptionQuantityDTO { SubscriptionId = 1, NewQuantity = 5 };
            var expectedResult = true;
            _mockSubscriptionService.Setup(s => s.ChangeSubscriptionQuantity(dto.SubscriptionId, dto.NewQuantity))
                .ReturnsAsync(Result.Success());

            // Act
            var result = await _controller.ChangeSubscriptionQuantity(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var changeSubscriptionQuantityResult = Assert.IsType<Result>(okResult.Value);
            Assert.Equal(expectedResult, changeSubscriptionQuantityResult.IsSuccess);
        }

        [Fact]
        public async Task ChangeSubscriptionQuantity_ReturnsBadRequest_OnException()
        {
            // Arrange
            var dto = new ChangeSubscriptionQuantityDTO { SubscriptionId = 1, NewQuantity = -1 };
            var errorMessage = "Error changing subscription quantity";
            _mockSubscriptionService.Setup(s => s.ChangeSubscriptionQuantity(dto.SubscriptionId, dto.NewQuantity))
                .ThrowsAsync(new Exception(errorMessage));

            // Act
            var result = await _controller.ChangeSubscriptionQuantity(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task ProvisionSubscription_ReturnsOkResult_OnSuccess()
        {
            // Arrange
            var subscription = new SubscriptionDTO {Id = 1 ,MaxQuantity = 1000, MinQuantity = 1, Quantity = 2 , SoftwareId = 2, SoftwareName = "Test software name 1", State = "Active", ValidTo = DateTime.Now.AddMonths(6), AccountId = 1};
            _mockSubscriptionService.Setup(s => s.ProvisionSubscription(It.IsAny<Subscription>()))
                .ReturnsAsync(Result<SubscriptionDTO>.Success(subscription));
            var request = new ProvisionSubscriptionDTO { AccountId = 1, Quantity = 1, SoftwareId = 1, State = "Active", ValidTo = DateTime.Now.AddMonths(6), SoftwareName = "Test software name1" };
            // Act
            var result = await _controller.ProvisionSubscription(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var subscriptionResult = Assert.IsType<Result<SubscriptionDTO>>(okResult.Value);
            Assert.Equal(subscription, subscriptionResult.Value);
        }

        [Fact]
        public async Task ProvisionSubscription_ReturnsBadRequest_OnException()
        {
            // Arrange
            var dto = new ProvisionSubscriptionDTO { AccountId = 1, Quantity = 1, SoftwareId = 1, SoftwareName = "Test software name 1", State = "AAAA", ValidTo = DateTime.Now.AddMonths(7)};
            var errorMessage = "State is invalid value. State can be: Active, Canceled ";
            _mockSubscriptionService.Setup(s => s.ProvisionSubscription(It.IsAny<Subscription>()))
                .ThrowsAsync(new Exception(errorMessage));

            // Act
            var result = await _controller.ProvisionSubscription(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }
    }
    }
