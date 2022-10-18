using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Net6.Domain
{

    // Szablon interfejsu
    public interface IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        bool Exists(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
    }

    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        Customer GetByEmail(string email);
    }

    public interface IProductRepository : IEntityRepository<Product>
    {
        Product GetByCode(string code);
        IEnumerable<Product> GetByColor(string color);
        IEnumerable<Product> GetByCustomer(int customerId);
    }
}
