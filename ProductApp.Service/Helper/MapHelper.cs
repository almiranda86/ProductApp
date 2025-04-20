using ProductApp.Domain.Commands;
using ProductApp.Domain.Model;
using ProductApp.Domain.Queries;
using System.Globalization;

namespace ProductApp.Service.Helper
{
    public static class MapHelper
    {
        public static Product MapDTOtoDomainModel(RegisterProductRequest registerProductRequest)
        {
            return new Product
            {
                Categories = registerProductRequest.Categories,
                Description = registerProductRequest.Description,
                Price = registerProductRequest.Price.ToString(),
            };
        }

        public static GetProductResult MapDomainToGetProductDtoModel(Product? product)
        {
            if(product == null)
                return new GetProductResult();

            return new GetProductResult
            {
                Id = Guid.Parse(product.Id),
                Categories = product.Categories,
                Description = product.Description,
                Price = float.Parse(product.Price, CultureInfo.InvariantCulture.NumberFormat),
                Stock = Convert.ToInt32(product.Stock)
            };
        }
    }
}
