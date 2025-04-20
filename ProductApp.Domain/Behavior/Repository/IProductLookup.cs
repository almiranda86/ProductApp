using ProductApp.Domain.Model;

namespace ProductApp.Domain.Behavior.Repository
{
    public interface IProductLookup
    {
        public Task<Product> GetProductById(string productId);
    }
}
