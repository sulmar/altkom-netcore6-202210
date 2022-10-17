using Altkom.Net6.Domain;

namespace Altkom.Net6.Infrastructure
{
    public abstract class InMemoryEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected IDictionary<int, TEntity> _entities;

        public void Add(TEntity entity)
        {
            int id = _entities.Keys.Max();

            entity.Id = ++id;

            _entities.Add(id, entity);
        }

        public bool Exists(int id)
        {
            return _entities.ContainsKey(id);   
        }

        public IEnumerable<TEntity> Get()
        {
            return _entities.Values;
        }

        public TEntity Get(int id)
        {
            _entities.TryGetValue(id, out TEntity entity);

            return entity;
        }

        public void Remove(int id)
        {
            _entities.Remove(id);
        }

        public void Update(TEntity entity)
        {
            int id = entity.Id;
            Remove(entity.Id);
            _entities.Add(id, entity);
        }
    }
}