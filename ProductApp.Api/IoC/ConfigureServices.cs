using ProductApp.Domain.Behavior.Service;
using ProductApp.Service.Manager;

namespace ProductApp.Api.IoC
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRegisterProduct, RegisterProductManager>();
            services.AddScoped<IGetProduct, GetProductManager>();


            return services;
        }
    }
}
