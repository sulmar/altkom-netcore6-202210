using Altkom.Net6.Domain;

namespace Altkom.Net6.Infrastructure
{
    public class InMemoryProductRepository : InMemoryEntityRepository<Product>, IProductRepository
    {
        public InMemoryProductRepository()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Code = "AAA", Color = "Red", Price = 20.99m },
                new Product { Id = 2, Name = "Product 2", Code = "BBB", Color = "Green", Price = 10.99m },
                new Product { Id = 3, Name = "Product 3", Code = "CCC", Color = "Blue", Price = 5.99m },
                
            };

            _entities = products.ToDictionary(p => p.Id); // Primary Key ;-)
        }

        public Product GetByCode(string code)
        {
            return _entities.Values.SingleOrDefault(e => e.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Product> GetByColor(string color)
        {
            return _entities.Values.Where(e => e.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Product> GetByCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}