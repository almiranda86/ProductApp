using ProductApp.Api.Middleware;

namespace ProductApp.Api.IoC
{
    public static class ConfigureGlobalExceptionsHandler
    {
        public static IServiceCollection AddGlobalExceptionsHandler(this IServiceCollection services)
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }

        public static IApplicationBuilder UseGlobalExceptionsHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler();

            return app;
        }
    }
}
