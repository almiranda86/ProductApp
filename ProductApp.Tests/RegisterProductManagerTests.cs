using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using Moq;
using ProductApp.Domain.Behavior.Repository;
using ProductApp.Domain.Commands;
using ProductApp.Domain.Model;
using ProductApp.Service.Manager;
using ProductApp.Service.Validations;

namespace ProductApp.Tests
{
    public class RegisterProductManagerTests
    {
        private readonly Mock<IProductPersister> _productPersisterMock;
        private readonly Mock<IValidator<RegisterProductRequest>> _validatorMock;
        private readonly Mock<ILogger<RegisterProductManager>> _loggerMock;
        private readonly RegisterProductManager _registerProductManager;

        private readonly IValidator<RegisterProductRequest> _actualValidator;

        public RegisterProductManagerTests()
        {
            _productPersisterMock = new Mock<IProductPersister>();
            _validatorMock = new Mock<IValidator<RegisterProductRequest>>();
            _loggerMock = new Mock<ILogger<RegisterProductManager>>();
            _registerProductManager = new RegisterProductManager(
                _productPersisterMock.Object,
                _validatorMock.Object,
            _loggerMock.Object);

            _actualValidator = new RegisterProductRequestValidation();
        }

        [Fact]
        public async Task RegisterProductAsync_ShouldReturnSuccess_WhenProductIsValidAndPersisted()
        {
            // Arrange
            var request = new RegisterProductRequest
            {
                Description = "Test Product",
                Categories = new List<string> { "Category1" },
                Price = 100.0f
            };

            // Simulate successful validation
            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<RegisterProductRequest>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new FluentValidation.Results.ValidationResult()); 


            _productPersisterMock
                .Setup(p => p.CreateProduct(It.IsAny<Product>()))
                .ReturnsAsync(true);

            // Act
            var result = await _registerProductManager.RegisterProductAsync(request);

            // Assert
            Assert.True(result.ResponseModel.Success);
            Assert.Equal("Product registered successfully", result.ResponseModel.Message);
        }

        [Fact]
        public async Task RegisterProductAsync_ShouldFailValidation_WhenRequestIsInvalid()
        {
            // Arrange
            var request = new RegisterProductRequest
            {
                Description = "",
                Categories = new List<string>(),
                Price = -1.0f
            };

            // Use TestValidate to validate the request
            var validationResult = _actualValidator.TestValidate(request);

            // Act & Assert
            validationResult.ShouldHaveValidationErrorFor(request => request.Price);
            validationResult.ShouldHaveValidationErrorFor(request => request.Categories);
        }

        [Fact]
        public async Task RegisterProductAsync_ShouldReturnFailure_WhenProductPersistenceFails()
        {
            // Arrange
            var request = new RegisterProductRequest
            {
                Description = "Test Product",
                Categories = new List<string> { "Category1" },
                Price = 100.0f
            };

            // Simulate successful validation
            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<RegisterProductRequest>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _productPersisterMock
                .Setup(p => p.CreateProduct(It.IsAny<Product>()))
                .ReturnsAsync(false);

            // Act
            var result = await _registerProductManager.RegisterProductAsync(request);

            // Assert
            Assert.False(result.ResponseModel.Success);
            Assert.NotNull(result.ResponseModel.Message);
            Assert.Equal("Product not created!", result.ResponseModel.Message);
        }
    }
}