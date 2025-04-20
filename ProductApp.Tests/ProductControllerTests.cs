using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductApp.Api.Controllers;
using ProductApp.Domain.Behavior.Service;
using ProductApp.Domain.Commands;
using ProductApp.Domain.Core.ResultPattern;
using ProductApp.Domain.Queries;
using System.Net;
namespace ProductApp.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<ILogger<ProductController>> _loggerMock;
        private readonly Mock<IRegisterProduct> _registerProductMock;
        private readonly Mock<IGetProduct> _getProductMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _loggerMock = new Mock<ILogger<ProductController>>();
            _registerProductMock = new Mock<IRegisterProduct>();
            _getProductMock = new Mock<IGetProduct>();
            _controller = new ProductController(_loggerMock.Object, _registerProductMock.Object, _getProductMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOk_WhenProductIsFound()
        {
            // Arrange
            var productId = "123";
            var productResult = new GetProductResult
            {
                Id = Guid.NewGuid(),
                Stock = 10,
                Description = "Test Product",
                Categories = new List<string> { "Category1" },
                Price = 100.0f
            };
            _getProductMock
                .Setup(x => x.GetProductAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<GetProductResult>.Ok(productResult));

            // Act
            var result = await _controller.Get(productId);

            // Assert
            var okResult = Assert.IsType<JsonResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(productResult, okResult.Value);
        }

        [Fact]
        public async Task Get_ShouldReturnInternalServerError_WhenGetProductFails()
        {
            // Arrange
            var productId = "123";
            _getProductMock
                .Setup(x => x.GetProductAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<GetProductResult>.Fail(new List<Error> { new Error("Error occurred") }));

            // Act
            var result = await _controller.Get(productId);

            // Assert
            var objectResult = Assert.IsType<JsonResult>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturnOk_WhenProductIsRegisteredSuccessfully()
        {
            // Arrange
            var request = new RegisterProductRequest
            {
                Description = "New Product",
                Categories = new List<string> { "Category1" },
                Price = 50.0f
            };
            var response = new RegisterProductResponse
            {
                Success = true,
                Message = "Product registered successfully"
            };
            _registerProductMock
                .Setup(x => x.RegisterProductAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<RegisterProductResponse>.Ok(response));

            // Act
            var result = await _controller.Post(request);

            // Assert
            var okResult = Assert.IsType<JsonResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(response.Message, okResult.Value);
        }

        [Fact]
        public async Task Post_ShouldReturnInternalServerError_WhenRegistrationFails()
        {
            // Arrange
            var request = new RegisterProductRequest
            {
                Description = "New Product",
                Categories = new List<string> { "Category1" },
                Price = 50.0f
            };
            _registerProductMock
                .Setup(x => x.RegisterProductAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<RegisterProductResponse>.Fail(new List<Error> { new Error("Error occurred") }));

            // Act
            var result = await _controller.Post(request);

            // Assert
            var objectResult = Assert.IsType<JsonResult>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturnInternalServerError_WhenResponseModelIndicatesFailure()
        {
            // Arrange
            var request = new RegisterProductRequest
            {
                Description = "New Product",
                Categories = new List<string> { "Category1" },
                Price = 50.0f
            };
            var response = new RegisterProductResponse
            {
                Success = false,
                Message = "Registration failed"
            };
            _registerProductMock
                .Setup(x => x.RegisterProductAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<RegisterProductResponse>.Ok(response));

            // Act
            var result = await _controller.Post(request);

            // Assert
            var objectResult = Assert.IsType<JsonResult>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
            Assert.Equal(response.Message, objectResult.Value);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenProductIdIsEmpty()
        {
            // Arrange
            var productId = string.Empty;

            var productResult = new GetProductResult
            {
                Id = Guid.Empty,
                Stock = 10,
                Description = "Test Product",
                Categories = new List<string> { "Category1" },
                Price = 100.0f
            };
            _getProductMock
                .Setup(x => x.GetProductAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<GetProductResult>.Ok(productResult));

            // Act
            var result = await _controller.Get(productId);

            // Assert
            var notFoundResult = Assert.IsType<JsonResult>(result);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }
    }
}
