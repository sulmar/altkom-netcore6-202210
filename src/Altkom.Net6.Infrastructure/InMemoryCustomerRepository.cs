using Altkom.Net6.Domain;

namespace Altkom.Net6.Infrastructure
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private IDictionary<int, Customer> _customers;

        public InMemoryCustomerRepository()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Smith", Email="john@domain.com", Gender = Gender.Male, Salary = 1000 },
                new Customer { Id = 2, FirstName = "Kate", LastName = "Smith", Email="kate@domain.com", Gender = Gender.Female, Salary = 2000 },
                new Customer { Id = 3, FirstName = "Bob", LastName = "Smith", Email="bob@domain.com", Gender = Gender.Male, Salary = 3000 },
            };

            _customers = customers.ToDictionary(p => p.Id); // Primary Key ;-)

            //_customers = new Dictionary<int, Customer>
            //{
            //    { 1, new Customer { Id = 1, FirstName = "John", LastName = "Smith", Email="john@domain.com", Gender = Gender.Male, Salary = 1000 }   },
            //    { 2, new Customer { Id = 2, FirstName = "Kate", LastName = "Smith", Email="kate@domain.com", Gender = Gender.Female, Salary = 2000 } },
            //    { 3, new Customer { Id = 3, FirstName = "Bob", LastName = "Smith", Email="bob@domain.com", Gender = Gender.Male, Salary = 3000 }     },
            //};
        }

        public void Add(Customer customer)
        {
            int id = _customers.Keys.Max();

            customer.Id = ++id;

            _customers.Add(id, customer);
        }

        public bool Exists(int id)
        {
            return _customers.ContainsKey(id);
        }

        public IEnumerable<Customer> Get()
        {
            return _customers.Values;
        }

        public Customer Get(int id)
        {
            _customers.TryGetValue(id, out Customer customer);

            return customer;

            //return _customers[id]; // Jeśli nie znajdzie to pojawia się Exception
        }

        public Customer GetByEmail(string email)
        {
            return _customers.Values.SingleOrDefault(c=>c.Email == email);  // Linq
        }

        public void Remove(int id)
        {
            _customers.Remove(id);
        }

        public void Update(Customer customer)
        {
            int id = customer.Id;
            Remove(customer.Id);            
            _customers.Add(id, customer);            
        }
    }
}