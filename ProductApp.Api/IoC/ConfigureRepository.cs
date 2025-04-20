using ProductApp.Domain.Behavior.Repository;
using ProductApp.MongoDB;
using ProductApp.MongoDB.Persister;

namespace ProductApp.Api.IoC
{
    public static class ConfigureRepository
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductPersister, ProductPersister>();
            services.AddScoped<IProductLookup, ProductLookup>();

            return services;
        }
    }
}
