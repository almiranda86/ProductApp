using MongoDB.Driver;
using ProductApp.MongoDB;
using ProductApp.MongoDB.Settings;

namespace ProductApp.Api.IoC
{
    public static class ConfigureMongoDBSContext
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(settings => configuration.GetSection("MongoDB").Bind(settings));
            services.AddSingleton<MongoDbContext>();

            return services;
        }
    }
}
