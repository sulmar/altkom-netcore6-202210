using Altkom.Net6.Domain;

namespace Altkom.Net6.Infrastructure
{
    public class DbCustomerRepository : ICustomerRepository
    {
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