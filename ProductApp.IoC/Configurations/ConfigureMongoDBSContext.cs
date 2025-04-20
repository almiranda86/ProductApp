using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.MongoDB;
using ProductApp.MongoDB.Settings;

namespace ProductApp.IoC.Configurations
{
    public static class ConfigureMongoDBSContext
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(settings => configuration.GetSection("MongoDB").Bind(settings));
            services.AddScoped<MongoDbContext>();

            return services;
        }
    }
}
