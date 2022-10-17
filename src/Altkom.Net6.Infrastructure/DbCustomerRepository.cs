using Altkom.Net6.Domain;

namespace Altkom.Net6.Infrastructure
{
    public class DbCustomerRepository : ICustomerRepository
    {
        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}