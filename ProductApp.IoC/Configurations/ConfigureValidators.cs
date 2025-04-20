using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Domain.Commands;
using ProductApp.Service.Validations;

namespace ProductApp.IoC.Configurations
{
    public static class ConfigureValidators
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterProductRequest>, RegisterProductRequestValidation>();
            return services;
        }

    }
}
