using FluentValidation;
using ProductApp.Domain.Commands;
using ProductApp.Service.Validations;

namespace ProductApp.Api.IoC
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
