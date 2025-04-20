using MongoDB.Driver;
using ProductApp.Domain.Behavior.Repository;
using ProductApp.Domain.Model;

namespace ProductApp.MongoDB
{
    public class ProductLookup : IProductLookup
    {
        private readonly MongoDbContext _mongoContext;
        public ProductLookup(MongoDbContext mongoContext)
        {
            _mongoContext = mongoContext;
        }
        public async Task<Product> GetProductById(string productId)
        {
            var response = await _mongoContext.Product.Find(p => p.Id.Equals(productId)).FirstOrDefaultAsync();

            return response;
        }
    }
}
