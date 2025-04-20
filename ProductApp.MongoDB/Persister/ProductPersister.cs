using MongoDB.Driver;
using ProductApp.Domain.Behavior.Repository;
using ProductApp.Domain.Model;

namespace ProductApp.MongoDB.Persister
{
    public class ProductPersister : IProductPersister
    {
        private readonly MongoDbContext _mongoContext;

        public ProductPersister(MongoDbContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<bool> CreateProduct(Product product)
        {
            //INSERT or UPDATE
            try
            {
                UpdateOptions updateOptions = new() { IsUpsert = true };
                var filter = DefineFilterCondition(product);
                var updateDefinition = DefineUpdateObject(product);

                await _mongoContext.Product.UpdateOneAsync(filter, updateDefinition, options: updateOptions);
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        private FilterDefinition<Product> DefineFilterCondition(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            return filter;
        }

        private UpdateDefinition<Product> DefineUpdateObject(Product product)
        {
            var updateBuilder = new UpdateDefinitionBuilder<Product>();

            var updateDefinition = updateBuilder.SetOnInsert(p => p.Price, product.Price)
                                                .SetOnInsert(p => p.Id, product.Id)
                                                .SetOnInsert(p => p.Categories, product.Categories)
                                                .SetOnInsert(p => p.Description, product.Description)
                                                .SetOnInsert(p => p.Stock, product.Stock);

            return updateDefinition;
        }
    }
}
