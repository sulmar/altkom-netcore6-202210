using Altkom.Net6.Domain;
using Altkom.Net6.Infrastructure;

namespace Altkom.Net6.MinimalApi
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // Rejestrowanie w konterze wstrzykiwania zależności

            services.AddSingleton<IProductRepository, InMemoryProductRepository>(); // Rejestrowanie w konterze wstrzykiwania zależności

            return services;
        }
    }
}
