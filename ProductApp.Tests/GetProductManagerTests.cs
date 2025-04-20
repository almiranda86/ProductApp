using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProductApp.Api.Extensions;
using ProductApp.Domain.Behavior.Repository;
using ProductApp.Domain.Model;
using ProductApp.Domain.Queries;
using ProductApp.Service.Manager;

namespace ProductApp.Tests
{
    public class GetProductManagerTests
    {
        private readonly Mock<IProductLookup> _productLookupMock;
        private readonly Mock<ILogger<GetProductManager>> _loggerMock;
        private readonly GetProductManager _getProductManager;

        public GetProductManagerTests()
        {
            _productLookupMock = new Mock<IProductLookup>();
            _loggerMock = new Mock<ILogger<GetProductManager>>();
            _getProductManager = new GetProductManager(_productLookupMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetProductAsync_ShouldReturnError_WhenProductIdIsNull()
        {
            // Arrange
            string productId = null;
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _getProductManager.GetProductAsync(productId, cancellationToken);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains(result.Errors, e => e.Message == "Product Id can not be empty!");
        }

        [Fact]
        public async Task GetProductAsync_ShouldReturnError_WhenProductIdIsEmpty()
        {
            // Arrange
            string productId = string.Empty;
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _getProductManager.GetProductAsync(productId, cancellationToken);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains(result.Errors, e => e.Message == "Product Id can not be empty!");
        }

        [Fact]
        public async Task GetProductAsync_ShouldEmptyModel_WhenProductNotFound()
        {
            // Arrange
            string productId = "nonexistent-id";
            var cancellationToken = CancellationToken.None;
            _productLookupMock.Setup(x => x.GetProductById(productId))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _getProductManager.GetProductAsync(productId, cancellationToken);

            // Assert
            Assert.False(result.IsFailed);
            Assert.True(result.ResponseModel.Id.IsEmpty());
        }

        [Fact]
        public async Task GetProductAsync_ShouldReturnMappedResult_WhenProductExists()
        {
            // Arrange
            string productId = "90700C24-1459-41AD-A16C-1A2756C7ADB0";
            var cancellationToken = CancellationToken.None;
            var product = new Product
            {
                Id = productId,
                Stock = "10",
                Description = "Test Product",
                Categories = new List<string> { "Category1", "Category2" },
                Price = "100.50"
            };
            _productLookupMock.Setup(x => x.GetProductById(productId))
                .ReturnsAsync(product);

            var expectedResult = new GetProductResult
            {
                Id = Guid.Parse(productId),
                Stock = 10,
                Description = "Test Product",
                Categories = new List<string> { "Category1", "Category2" },
                Price = 100.5f
            };

            // Act
            var result = await _getProductManager.GetProductAsync(productId, cancellationToken);

            // Assert
            Assert.True(result.IsSuccessful);
            expectedResult.Should().BeEquivalentTo(result.ResponseModel);
        }
    }
}
