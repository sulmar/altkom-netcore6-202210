using Altkom.Net6.Domain;

namespace Altkom.Net6.Infrastructure
{

    public class InMemoryCustomerRepository : InMemoryEntityRepository<Customer>, ICustomerRepository
    {
        public InMemoryCustomerRepository()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Smith", Email="john@domain.com", Gender = Gender.Male, Salary = 1000 },
                new Customer { Id = 2, FirstName = "Kate", LastName = "Smith", Email="kate@domain.com", Gender = Gender.Female, Salary = 2000 },
                new Customer { Id = 3, FirstName = "Bob", LastName = "Smith", Email="bob@domain.com", Gender = Gender.Male, Salary = 3000 },
            };

            _entities = customers.ToDictionary(p => p.Id); // Primary Key ;-)

        }
     
        public Customer GetByEmail(string email)
        {
            return _entities.Values.SingleOrDefault(c => c.Email == email);  // Linq
        }

    }
}