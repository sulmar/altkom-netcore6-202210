using Altkom.Net6.Domain;
using Altkom.Net6.Domain.Validators;
using Altkom.Net6.Infrastructure;
using FluentValidation;

namespace Altkom.Net6.MinimalApi
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // Rejestrowanie w konterze wstrzykiwania zależności

            services.AddSingleton<IProductRepository, InMemoryProductRepository>(); // Rejestrowanie w konterze wstrzykiwania zależności

            services.AddScoped<IValidator<Customer>, CustomerValidator>();

            return services;
        }
    }
}
