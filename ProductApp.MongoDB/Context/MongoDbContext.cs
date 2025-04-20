using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductApp.Domain.Model;
using ProductApp.MongoDB.Settings;

namespace ProductApp.MongoDB
{
    public class MongoDbContext
    {
        private readonly IMongoCollection<Product> _productCollection;

        public MongoDbContext(IOptions<MongoDBSettings> mongoDBSettings, IMongoCollection<Product> productCollection = null)
        {
            if (productCollection != null)
            {
                _productCollection = productCollection;
            }
            else
            {
                var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
                var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
                _productCollection = database.GetCollection<Product>(mongoDBSettings.Value.CollectionName);
            }
        }
        public IMongoCollection<Product> Product => _productCollection;
    }
}
