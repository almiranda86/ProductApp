using FluentValidation;
using Microsoft.Extensions.Logging;
using ProductApp.Domain.Behavior.Repository;
using ProductApp.Domain.Behavior.Service;
using ProductApp.Domain.Commands;
using ProductApp.Domain.Core.ResultPattern;
using ProductApp.Service.Helper;

namespace ProductApp.Service.Manager
{
    public class RegisterProductManager : IRegisterProduct
    {
        private readonly IProductPersister _productPersister;
        private readonly IValidator<RegisterProductRequest> _validator;
        private readonly ILogger<RegisterProductManager> _logger;

        public RegisterProductManager(IProductPersister productPersister,
                                      IValidator<RegisterProductRequest> validator,
                                      ILogger<RegisterProductManager> logger)
        {
            _productPersister = productPersister ?? throw new ArgumentNullException(nameof(productPersister));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<RegisterProductResponse>> RegisterProductAsync(RegisterProductRequest registerProductRequest, CancellationToken cancellationToken = default)
        {
            var response = new RegisterProductResponse();

            await _validator.ValidateAndThrowAsync(registerProductRequest, cancellationToken);

            var product = CreateProductPersistModel(registerProductRequest);

            if (await _productPersister.CreateProduct(product))
            {
                response.Success = true;
                response.Message = "Product registered successfully";
            }

            return response;
        }

        private static Domain.Model.Product CreateProductPersistModel(RegisterProductRequest registerProductRequest)
        {
            var product = MapHelper.MapDTOtoDomainModel(registerProductRequest);
            product.Id = GuidIdGenerator.GenerateId();
            product.Stock = "1";
            return product;
        }
    }
}
