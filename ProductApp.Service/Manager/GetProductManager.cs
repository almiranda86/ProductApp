using Microsoft.Extensions.Logging;
using ProductApp.Domain.Behavior.Repository;
using ProductApp.Domain.Behavior.Service;
using ProductApp.Domain.Core.ResultPattern;
using ProductApp.Domain.Queries;
using ProductApp.Service.Helper;

namespace ProductApp.Service.Manager
{
    public class GetProductManager : IGetProduct
    {
        private readonly IProductLookup _productLookup;

        public GetProductManager(IProductLookup productLookup, ILogger<GetProductManager> logger)
        {
            _productLookup = productLookup ?? throw new ArgumentNullException(nameof(productLookup));
        }
        public async Task<Result<GetProductResult>> GetProductAsync(string productId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(productId))
                return new Error($"Product Id can not be empty!");

            var product = await _productLookup.GetProductById(productId);

            var result = MapHelper.MapDomainToGetProductDtoModel(product);
            return result;
        }
    }
}
